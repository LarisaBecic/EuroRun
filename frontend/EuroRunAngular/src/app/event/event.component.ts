import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute, Router } from "@angular/router";
import { Config } from '../config';
import { Event } from '../model/Event.model';
import { EventRegistrationAdd } from '../model/EventRegistration.model';
import { Gender } from '../model/Gender.model';
import { LoginInfo, UserAccount } from '../helpers/login-info';
import { AuthService } from '../helpers/auth.service';

@Component({
    selector: 'app-event',
    templateUrl: './event.component.html',
    styleUrl: './event.component.css',
})
export class EventComponent implements OnInit {

    event: Event | null = null;
    loginInfo: LoginInfo | null = null;

    genders: Gender[] = [];

    wizardStep = 0;
    showWizard = false;

    firstName = '';
    lastName = '';
    email = '';
    phoneNumber = '';
    dateOfBirth = '';
    gender_id: number | null = null;

    club = '';
    shirtSize = '';
    numberOfFinishedRaces: number | null = null;
    eventDiscoverySource = '';
    note = '';

    constructor(
        private httpClient: HttpClient,
        private route: ActivatedRoute,
        private router: Router,
        private authService: AuthService
    ) { }

    ngOnInit(): void {
        this.authService.loginInfo$.subscribe(info => {
            this.loginInfo = info;
            if (info?.authentificationToken) {
                this.syncUserToForm(info.authentificationToken.userAccount);
            }

        });

        const eventId = Number(this.route.snapshot.paramMap.get('id'));
        if (eventId) {
            this.getEventById(eventId);
        }

        this.getGenders();
    }

    private syncUserToForm(u: UserAccount) {
        this.firstName = u.firstName ?? '';
        this.lastName = u.lastName ?? '';
        this.email = u.email ?? '';
        this.phoneNumber = u.phoneNumber ?? '';
        this.dateOfBirth = u.dateOfBirth ?? '';
        this.gender_id = u.gender?.id ?? null;
    }

    getEventById(id: number) {
        this.httpClient
            .get<Event>(Config.api + '/Event/GetById/id?id=' + id, Config.http_options())
            .subscribe(res => this.event = res);
    }

    getGenders() {
        this.httpClient
            .get<Gender[]>(Config.api + '/Gender/GetAll', Config.http_options())
            .subscribe(res => this.genders = res);
    }

    openWizard() {
        if (this.loginInfo?.authentificationToken) {
            this.syncUserToForm(this.loginInfo.authentificationToken.userAccount);
        }

        this.wizardStep = 1;
        this.showWizard = true;
    }

    closeWizard() {
        this.showWizard = false;
        this.wizardStep = 0;
    }

    updateUser() {
        if (!this.loginInfo?.authentificationToken) return;

        const token = this.loginInfo.authentificationToken;
        const u = token.userAccount;

        const payload = {
            firstName: this.firstName ?? u.firstName,
            lastName: this.lastName ?? u.lastName,
            phoneNumber: this.phoneNumber ?? u.phoneNumber,
            email: this.email ?? u.email,
            userName: u.userName,
            picture: null,
            active: true,
            dateOfBirth: this.dateOfBirth ?? u.dateOfBirth,
            gender_id: this.gender_id ?? u.gender?.id
        };

        this.httpClient
            .put(
                Config.api + '/UserAccount/Update/id?id=' + u.id,
                payload,
                Config.http_options()
            )
            .subscribe({
                next: () => {
                    const updatedUser: UserAccount = {
                        ...u,
                        firstName: payload.firstName,
                        lastName: payload.lastName,
                        phoneNumber: payload.phoneNumber,
                        email: payload.email,
                        dateOfBirth: payload.dateOfBirth,
                        gender: this.genders.find(g => g.id === payload.gender_id)
                    };

                    this.authService.updateUserAccount(updatedUser);
                    this.syncUserToForm(updatedUser);
                },
                error: err => alert(err.error || 'Failed to update user')
            });
    }


    addEventRegistration() {
        if (!this.loginInfo?.authentificationToken || !this.event) return;

        const reg: EventRegistrationAdd = {
            userAccount_id: this.loginInfo.authentificationToken.userAccount.id,
            event_id: this.event.id,
            club: this.club,
            shirtSize: this.shirtSize,
            numberOfFinishedRaces: this.numberOfFinishedRaces ?? undefined,
            eventDiscoverySource: this.eventDiscoverySource,
            note: this.note
        };

        this.httpClient
            .post(Config.api + '/EventRegistration/Add', reg, Config.http_options())
            .subscribe(() => {
                this.closeWizard();
                alert('Successfully registered!');
            });
    }
}
