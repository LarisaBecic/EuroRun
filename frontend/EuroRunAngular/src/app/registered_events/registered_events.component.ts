import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Config } from '../config';
import { EventRegistration } from '../model/EventRegistration.model';
import { AuthService } from '../helpers/auth.service';
import { LoginInfo } from '../helpers/login-info';

@Component({
  selector: 'app-registered-events',
  templateUrl: './registered_events.component.html',
  styleUrl: './registered_events.component.css',
})
export class RegisteredEventsComponent implements OnInit {

  registeredEvents: EventRegistration[] = [];

  loginInfo: LoginInfo | null = null;
  user_id: number | null = null;

  selectedRegistration: EventRegistration | null = null;
  isDetailsOpen = false;

  isQRcodeOpen = false;

  qrValue: string = "";

  constructor(private httpClient: HttpClient, private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.loginInfo$.subscribe(info => {
      this.loginInfo = info;
      if (info?.authentificationToken) {
        this.user_id = info.authentificationToken.userAccount.id;
        this.getEventRegistrations(this.user_id);
      }
    });
  }

  getEventRegistrations(userId: number) {
    this.httpClient
      .get<EventRegistration[]>(
        Config.api + '/EventRegistration/GetByUserId/userAccountId?userAccountId=' + userId,
        Config.http_options()
      )
      .subscribe(res => this.registeredEvents = res);
  }

  // NEW
  openDetails(reg: EventRegistration) {
    this.selectedRegistration = reg;
    this.isDetailsOpen = true;
  }

  closeDetails() {
    this.isDetailsOpen = false;
    this.selectedRegistration = null;
  }

  openQRcode(id: number) {
    this.qrValue = `${window.location.origin}/admin/registration/${id}`;
    this.isQRcodeOpen = true;
  }

  closeQRcode() {
    this.isQRcodeOpen = false;
    this.qrValue = "";
  }

  updateComingSoon() {
    alert('Update feature coming soon 🚧');
  }
}

