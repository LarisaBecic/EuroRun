import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'EuroRunAngular';
  Logo: string | null = null;

  constructor(private http: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
    this.GetLogo();
  } 

  GetLogo(): void {
    const headers = new HttpHeaders({
      'Accept': 'image/png'
    });

    this.http.get("https://localhost:7249/Logo/Get", { headers, responseType: 'blob' })
      .subscribe(response => {
        const objectURL = URL.createObjectURL(response);
        this.Logo = objectURL;
      });
  }

 
}