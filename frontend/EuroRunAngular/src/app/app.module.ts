import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { MarkdownModule } from 'ngx-markdown';
import { RouterModule } from '@angular/router';
import { provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
        AppComponent,
        HomeComponent,
    ],
    bootstrap: [AppComponent],
    imports: [
      BrowserModule,
      MarkdownModule.forRoot(),
      RouterModule.forRoot([
            { path: '', component: HomeComponent },
            { path: '**', redirectTo: '', pathMatch: 'full' }
        ]),
      FormsModule,
    ],
  providers: [provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
