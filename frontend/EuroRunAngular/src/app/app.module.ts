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
import { AdminCitiesCountriesComponent } from './admin_cities_countries/admin_cities_countries.component';
import { AdminEventsComponent } from './admin_events/admin_events.component';

@NgModule({
  declarations: [
        AppComponent,
        HomeComponent,
        AuthComponent,
        AdminCitiesCountriesComponent,
        AdminEventsComponent
    ],
    bootstrap: [AppComponent],
    imports: [
      BrowserModule,
      FormsModule,
      MarkdownModule.forRoot(),
      RouterModule.forRoot([
            { path: '', component: HomeComponent },
            { path: 'auth', component: AuthComponent },
            { path: 'admin-countries-cities', component: AdminCitiesCountriesComponent },
            { path: 'admin-events', component: AdminEventsComponent },
            { path: '**', redirectTo: '', pathMatch: 'full' }
        ]),
    ],
  providers: [provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
