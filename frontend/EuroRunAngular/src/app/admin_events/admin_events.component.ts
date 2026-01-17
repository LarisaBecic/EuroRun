import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { Config } from '../config';
import { EventAddUpdate, Event } from '../model/Event.model';
import { Location } from '../model/Location.model';
import { EventType } from '../model/EventType.model';

@Component({
  selector: 'app-admin-events',
  templateUrl: './admin_events.component.html',
  styleUrl: './admin_events.component.css',
})
export class AdminEventsComponent implements OnInit {

  events: Event[] = [];
  locations: Location[] = [];
  eventTypes: EventType[] = [];

  selectedEvent: Event | null = null;
  isFormOpen = false;
  isEditMode = false;

  eventForm: EventAddUpdate = {
    name: '',
    eventType_id: 0,
    location_id: 0,
    dateTime: '',
    description: '',
    registrationDeadline: ''
  };

  constructor(private httpClient: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.getEvents();
    this.getLocations();
    this.getEventTypes();
  }

  getLocations() {
    this.httpClient
      .get<Location[]>(Config.api + '/Location/GetAll', Config.http_options())
      .subscribe(res => this.locations = res);
  }

  getEvents() {
    this.httpClient
      .get<Event[]>(Config.api + '/Event/GetAll', Config.http_options())
      .subscribe(res => this.events = res);
  }

  getEventTypes() {
    this.httpClient
      .get<EventType[]>(Config.api + '/EventType/GetAll', Config.http_options())
      .subscribe(res => this.eventTypes = res);
  }

  openAddForm() {
    this.isEditMode = false;
    this.isFormOpen = true;
    this.selectedEvent = null;

    this.eventForm = {
      name: '',
      eventType_id: 0,
      location_id: 0,
      dateTime: '',
      description: '',
      registrationDeadline: ''
    };
  }

  openEditForm(event: Event) {
    this.isEditMode = true;
    this.isFormOpen = true;
    this.selectedEvent = event;

    this.eventForm = {
      name: event.name,
      eventType_id: event.eventType_id,
      location_id: event.location_id,
      dateTime: event.dateTime,
      description: event.description,
      registrationDeadline: event.registrationDeadline
    };
  }

  closeForm() {
    this.isFormOpen = false;
    this.selectedEvent = null;
  }

  addEvent() {
    this.httpClient
      .post(Config.api + '/Event/Add', this.eventForm, Config.http_options())
      .subscribe(() => {
        this.getEvents();
        this.closeForm();
      });
  }

  updateEvent() {
    if (!this.selectedEvent) return;

    this.httpClient
      .put(
        Config.api + `/Event/Update/id?id=${this.selectedEvent.id}`,
        this.eventForm,
        Config.http_options()
      )
      .subscribe(() => {
        this.getEvents();
        this.closeForm();
      });
  }

  deleteEvent(id: number) {
    if (!confirm('Delete this event?')) return;

    this.httpClient
      .delete(Config.api + `/Event/Delete/id?id=${id}`, Config.http_options())
      .subscribe(() => this.getEvents());
  }

onDateTimeChange(field: 'dateTime' | 'registrationDeadline', value: string) {
  if (!value) return;

  const iso = new Date(value).toISOString();
  this.eventForm[field] = iso;
}


}
