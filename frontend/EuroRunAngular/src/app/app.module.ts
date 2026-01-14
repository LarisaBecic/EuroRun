import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { MarkdownModule } from 'ngx-markdown';
import { RouterModule } from '@angular/router';
import { provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";
import { FormsModule } from '@angular/forms';
import { AuthComponent } from './auth/auth.component';

@NgModule({
  declarations: [
        AppComponent,
        HomeComponent,
        AuthComponent
    ],
    bootstrap: [AppComponent],
    imports: [
      BrowserModule,
      FormsModule,
      MarkdownModule.forRoot(),
      RouterModule.forRoot([
            { path: '', component: HomeComponent },
            { path: 'auth', component: AuthComponent },
            { path: '**', redirectTo: '', pathMatch: 'full' }
        ]),
      FormsModule,
    ],
  providers: [provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
