import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Event as AppEvent} from '../model/Event.model'
import { Config } from '../config';
import { AuthService } from '../helpers/auth.service';
import { LoginInfo } from '../helpers/login-info';

@Component({
  selector: 'app-search-results',
  templateUrl: './favourite_events.component.html',
  styleUrl: './favourite_events.component.css'
})
export class FavouriteEventsComponent implements OnInit {

  events: AppEvent[] = [];

  loginInfo: LoginInfo | null = null;
  user_id: number | null = null;

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.authService.loginInfo$.subscribe(info => {
      this.loginInfo = info;
      if (info?.authentificationToken) {
        this.user_id = info.authentificationToken.userAccount.id;
      }
    });

    this.loadEvents();
  }

  loadEvents() {
    let url: string = `/Event/GetUserFavourites?UserId=${this.user_id}`;
    this.http
      .get<AppEvent[]>(Config.api + url)
      .subscribe(res => this.events = res);
  }
}
