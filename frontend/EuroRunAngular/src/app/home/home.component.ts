import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Event} from '../model/Event.model'
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})

export class HomeComponent implements OnInit{

  eventlist: Event[] | null=null;
  city: string = '';
  citytemp: string = '';


  constructor(private http: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
    
  } 

  GetEvents(city:string) {
    const headers = new HttpHeaders({
      'Accept': 'text/plain'
    });
  
    this.http.get<Event[]>("https://localhost:7249/Event/SearchEvents/search?city="+city, { headers })
    .subscribe(response => {
      this.eventlist = response
      this.citytemp=city
    });
  }
}
