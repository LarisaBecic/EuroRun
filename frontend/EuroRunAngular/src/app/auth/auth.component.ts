import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { LoginInfo } from '../helpers/login-info';
import { Config } from '../config';
import { AuthentificationHelper } from '../helpers/authentification-helper';
import { AuthService } from '../helpers/auth.service';
import { Gender } from '../model/Gender.model';

@Component({
    selector: 'app-auth',
    templateUrl: './auth.component.html',
    styleUrl: './auth.component.css',
})
export class AuthComponent implements OnInit {

    genders: Gender[] = [];

    txtUserName: any;
    txtPassword: any;
    txtEmail: any;
    txtUsernameRegister: any;
    txtFirstName: any;
    txtLastName: any;
    txtPasswordRegister: any;
    txtPhoneNumber: any;
    dateOfBirth: any;
    gender_id: any;
    active: boolean = true;
    role: number = 1;

    constructor(private httpClient: HttpClient, private router: Router, private authService: AuthService) {
    }

    ngOnInit(): void {
        this.getGenders();
    }

    logIn() {
        let credentials: any = {
            userName: this.txtUserName,
            password: this.txtPassword
        };

        if (this.txtUserName && this.txtPassword) {
            this.httpClient.post<LoginInfo>(Config.api + "/Authentification/Login", credentials)
                .subscribe({
                    next: (x: LoginInfo) => {
                        if (x.isLoggedIn) {
                            this.authService.setLoginInfo(x);
                            this.router.navigate([''], {
                                state: { message: 'Welcome back!' }
                            });
                        } else {
                            AuthentificationHelper.setLoginInfo(null);
                            alert("Incorrect login!");
                        }
                    },
                    error: (error) => {
                        if (error.status === 410) {
                            alert("User account is inactive.");
                        } else if (error.status === 403) {
                            alert(error.error);
                        } else {
                            alert("Server error. Try again.");
                        }
                    }
                });
        }
        else {
            alert("Please provide a username and a password!");
        }
    }

    register() {
        let registerInfo = {
            firstName: this.txtFirstName,
            lastName: this.txtLastName,
            phoneNumber: this.txtPhoneNumber,
            email: this.txtEmail,
            userName: this.txtUsernameRegister,
            picture: null,
            password: this.txtPasswordRegister,
            active: this.active,
            dateOfBirth: this.dateOfBirth,
            gender_id: this.gender_id,
            role_id: this.role
        };

        if (this.txtFirstName && this.txtLastName && this.txtPhoneNumber && this.txtEmail && this.txtUsernameRegister && this.txtPasswordRegister) {
            this.httpClient.post<void>(Config.api + "/UserAccount/Add", registerInfo).subscribe({
                next: () => {
                    this.router.navigate([''], {
                        state: { message: 'Registration successful! You can now log in.' }
                    });
                },
                error: (error) => {
                    alert(error.error);
                }
            })
        }
        else {
            alert("Please provide all the user data!");
        }
    }

    getGenders() {
        this.httpClient
            .get<Gender[]>(Config.api + '/Gender/GetAll', Config.http_options())
            .subscribe(res => this.genders = res);
    }

    switchTab(tabId: string, event: Event): void {
        event.preventDefault();

        const tabs = document.querySelectorAll('.tabs-content > div');
        tabs.forEach(tab => {
            tab.classList.remove('active');
        });

        const selectedTab = document.getElementById(tabId);
        if (selectedTab) {
            selectedTab.classList.add('active');
        }

        const tabLinks = document.querySelectorAll('.tabs h3 a');
        tabLinks.forEach(link => {
            link.classList.remove('active');
        });

        const clickedLink = event.target as HTMLElement;
        clickedLink.classList.add('active');
    }

    onDateTimeChange(field: 'dateOfBirth', value: string) {
        if (!value) return;

        const iso = new Date(value).toISOString();
        this.dateOfBirth[field] = iso;
    }

}
