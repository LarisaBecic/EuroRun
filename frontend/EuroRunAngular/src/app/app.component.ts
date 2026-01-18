import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHeaders } from '@angular/common/http';
import { Config } from './config';
import { LoginInfo } from './helpers/login-info'; import { AuthService } from './helpers/auth.service';
;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'EuroRunAngular';
  Logo: string | null = null;
  loginInfo: LoginInfo | null = null;

  searchCity = '';
  menuOpen = false;

  constructor(private httpClient: HttpClient, public router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.loginInfo$.subscribe(info => {
      this.loginInfo = info;
    });
    this.GetLogo();
  }

  GetLogo(): void {
    const headers = new HttpHeaders({
      'Accept': 'image/png'
    });

    this.httpClient.get("https://localhost:7249/Logo/Get", { headers, responseType: 'blob' })
      .subscribe(response => {
        const objectURL = URL.createObjectURL(response);
        this.Logo = objectURL;
      });
  }

  logOut() {
    const confirmed = window.confirm('Are you sure you want to log out?');

    if (!confirmed) {
      return; 
    }

    const token = Config.http_options();

    this.authService.setLoginInfo(null);

    this.httpClient
      .post(Config.api + '/Authentification/LogOut/', null, token)
      .subscribe();

    this.menuOpen = false;
    this.router.navigate(['']);
  }


  search() {
    if (!this.searchCity.trim()) return;
    this.router.navigate(['/results'], {
      queryParams: { city: this.searchCity }
    });
  }
}