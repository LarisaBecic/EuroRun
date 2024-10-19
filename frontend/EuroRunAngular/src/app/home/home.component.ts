import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Grad } from '../model/grad.model';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})

export class HomeComponent implements OnInit{

  gradoviLista: Grad[] | null=null;

  constructor(private http: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
    this.GetGradovi();
  } 

  GetGradovi() {
    const headers = new HttpHeaders({
      'Accept': 'text/plain'
    });
  
    this.http.get<Grad[]>("https://localhost:7249/Grad/GetAll", { headers })
      .subscribe(x => {
        this.gradoviLista = x;
      });
  }
}