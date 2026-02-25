import { Component, HostListener, OnInit } from '@angular/core';
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
    templateUrl: './admin_registration.component.html',
    styleUrl: './admin_registration.component.css',
})
export class AdminRegistrationComponent implements OnInit {

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

    user_id: number | null = null;
    event_id: number | null = null;

    club = '';
    shirtSize = '';
    numberOfFinishedRaces: number | null = null;
    eventDiscoverySource = '';
    note = '';

    zoomImage: string | null = null;

    scale = 1;
    posX = 0;
    posY = 0;

    private isPanning = false;
    private startX = 0;
    private startY = 0;

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
                this.user_id = info.authentificationToken.userAccount.id;
            }

        });

        this.event_id = Number(this.route.snapshot.paramMap.get('id'));
        if (this.event_id) {
            this.getEventById(this.event_id);
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
        let url: string = "";
        if (this.user_id != null) {
            url = '/Event/GetById/id?id=' + id + '&UserId=' + this.user_id;
        }
        else {
            url = '/Event/GetById/id?id=' + id;
        }

        this.httpClient
            .get<Event>(Config.api + url, Config.http_options())
            .subscribe(res => this.event = res);
    }

    toggleFavourite(eventClick: MouseEvent) {
        eventClick.stopPropagation();

        if (!this.user_id) {
            alert("You must be logged in.");
            return;
        }

        const body = {
            user_Id: this.user_id,
            event_Id: this.event_id
        };

        if (this.event!.userFavourite) {
            this.httpClient
                .delete(Config.api + '/FavouriteEvent/Delete', {
                    ...Config.http_options(),
                    body: body
                })
                .subscribe(() => {
                    this.event!.userFavourite = false;
                    this.event!.favouritedTimes--;
                });
        } else {
            this.httpClient
                .post(Config.api + '/FavouriteEvent/Add', body, Config.http_options())
                .subscribe(() => {
                    this.event!.userFavourite = true;
                    this.event!.favouritedTimes++;
                });
        }
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


    /*addEventRegistration() {
        if (!this.loginInfo?.authentificationToken || !this.event) return;

        const reg: EventRegistrationAdd = {
            userAccount_id: this.loginInfo.authentificationToken.userAccount.id,
            event_id: this.event.id,
            club: this.club,
            shirtSize: this.shirtSize,
            numberOfFinishedRaces: this.numberOfFinishedRaces ?? undefined,
            eventDiscoverySource: this.eventDiscoverySource,
            note: this.note,
            
        };

        this.httpClient
            .post(Config.api + '/EventRegistration/Add', reg, Config.http_options())
            .subscribe(() => {
                this.closeWizard();
                alert('Successfully registered!');
            });
    }*/

    get transformStyle() {
        return `translate(${this.posX}px, ${this.posY}px) scale(${this.scale})`;
    }

    openZoom(img: string) {
        this.zoomImage = img;
        this.scale = 1;
        this.posX = 0;
        this.posY = 0;
    }

    closeZoom() {
        this.zoomImage = null;
    }

    onWheel(event: WheelEvent) {
        event.preventDefault();

        const zoomSpeed = 0.1;

        if (event.deltaY < 0) {
            this.scale += zoomSpeed;
        } else {
            this.scale -= zoomSpeed;
        }

        this.scale = Math.min(Math.max(this.scale, 0.5), 5);
    }

    startPan(event: MouseEvent) {
        this.isPanning = true;
        this.startX = event.clientX - this.posX;
        this.startY = event.clientY - this.posY;
    }

    onPan(event: MouseEvent) {
        if (!this.isPanning) return;

        this.posX = event.clientX - this.startX;
        this.posY = event.clientY - this.startY;
    }

    stopPan() {
        this.isPanning = false;
    }

    @HostListener('document:keydown.escape')
    handleEscape() {
        this.closeZoom();
    }
}
