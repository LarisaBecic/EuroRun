import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Event as AppEvent} from '../model/Event.model'
import { Config } from '../config';

@Component({
  selector: 'app-search-results',
  templateUrl: './search_results.component.html',
  styleUrl: './search_results.component.css'
})
export class SearchResultsComponent implements OnInit {

  events: AppEvent[] = [];
  city = '';

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.city = params['city'];
      if (this.city) this.load();
    });
  }

  load() {
    this.http
      .get<AppEvent[]>(Config.api + `/Event/SearchEvents/search?city=${this.city}`)
      .subscribe(res => this.events = res);
  }
}
