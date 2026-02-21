import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Event as AppEvent } from '../model/Event.model'
import { HttpHeaders } from '@angular/common/http';
import { Config } from '../config';
import { LoginInfo } from '../helpers/login-info';
import { AuthService } from '../helpers/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})

export class HomeComponent implements OnInit {

  eventlist: Event[] | null = null;
  city: string = '';
  citytemp: string = '';

  events: AppEvent[] = [];

  loginInfo: LoginInfo | null = null;
  user_id: number | null = null;

  constructor(private httpClient: HttpClient, private authService: AuthService) {
  }

  ngOnInit(): void {
    this.authService.loginInfo$.subscribe(info => {
      this.loginInfo = info;
      if (info?.authentificationToken) {
        this.user_id = info.authentificationToken.userAccount.id;
      }
    });

    const nav = history.state;
    if (nav?.message) {
      alert(nav.message);
    }
    this.getTopTenEvents();
  }

  getTopTenEvents() {
    let url: string = "";
    if (this.user_id != null) {
      url = '/Event/GetTop10Events?UserId=' + this.user_id;
    }
    else {
      url = '/Event/GetTop10Events';
    }

    this.httpClient
      .get<AppEvent[]>(Config.api + url, Config.http_options())
      .subscribe(res => this.events = res);
  }

  @ViewChild('scrollContainer') scrollContainer!: ElementRef;

  scrollLeft() {
    this.scrollContainer.nativeElement.scrollBy({
      left: -300,
      behavior: 'smooth'
    });
  }

  scrollRight() {
    this.scrollContainer.nativeElement.scrollBy({
      left: 300,
      behavior: 'smooth'
    });
  }

}
