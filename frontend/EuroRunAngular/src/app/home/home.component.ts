import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Event as AppEvent} from '../model/Event.model'
import { HttpHeaders } from '@angular/common/http';
import { Config } from '../config';

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

  constructor(private httpClient: HttpClient) {
  }

  ngOnInit(): void {
    const nav = history.state;
    if (nav?.message) {
      alert(nav.message);
    }
    this.getTopFiveEvents();
  }

  getTopFiveEvents() {
    this.httpClient
      .get<AppEvent[]>(Config.api + '/Event/GetFiveEvents', Config.http_options())
      .subscribe(res => this.events = res);
  }
}
