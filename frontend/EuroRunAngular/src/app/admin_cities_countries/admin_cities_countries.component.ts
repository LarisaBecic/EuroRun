import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { Config } from '../config';
import { CityAddUpdate, City } from '../model/City.model';
import { Country } from '../model/Country.model';
import { Location, LocationAddUpdate } from '../model/Location.model';

@Component({
  selector: 'app-admin-cities-countries',
  templateUrl: './admin_cities_countries.component.html',
  styleUrl: './admin_cities_countries.component.css',
})
export class AdminCitiesCountriesComponent implements OnInit {

  countries: Country[] = [];
  cities: City[] = [];
  locations: Location[] = [];

  newCountryName = '';
  editCountryId: number | null = null;
  editCountryName = '';

  newCityName = '';
  newCityCountryId: number | null = null;

  editCityId: number | null = null;
  editCityName = '';
  editCityCountryId: number | null = null;

  newLocationName = '';
  newLocationLatitude: number | null = null;
  newLocationLongitude: number | null = null;
  newLocationCityId: number | null = null;

  editLocationId: number | null = null;
  editLocationName = '';
  editLocationLatitude: number | null = null;
  editLocationLongitude: number | null = null;
  editLocationCityId: number | null = null;

  constructor(private httpClient: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.getCountries();
    this.getCities();
    this.getLocations();
  }

  getCountries() {
    this.httpClient.get<Country[]>(Config.api + '/Country/GetAll')
      .subscribe({
        next: data => this.countries = data,
        error: err => alert(err.error || 'Failed to load countries')
      });
  }

  addCountry() {
    if (!this.newCountryName) return alert('Country name required');

    this.httpClient.post<void>(
      Config.api + '/Country/Add',
      { name: this.newCountryName }
    ).subscribe(() => {
      this.newCountryName = '';
      this.getCountries();
    });
  }

  startEditCountry(c: Country) {
    this.editCountryId = c.id;
    this.editCountryName = c.name;
  }

  updateCountry() {
    if (this.editCountryId == null || !this.editCountryName) return;

    this.httpClient.put<void>(
      Config.api + `/Country/Update/id?id=${this.editCountryId}`,
      { name: this.editCountryName }
    ).subscribe(() => {
      this.cancelEditCountry();
      this.getCountries();
      this.getCities();
      this.getLocations();
    });
  }

  deleteCountry(id: number) {
    if (!confirm('Delete this country?')) return;

    this.httpClient.delete<void>(
      Config.api + `/Country/Delete/id?id=${id}`
    ).subscribe(() => {
      this.getCountries();
      this.getCities();
      this.getLocations();
    });
  }

  cancelEditCountry() {
    this.editCountryId = null;
    this.editCountryName = '';
  }

  getCities() {
    this.httpClient.get<City[]>(Config.api + '/City/GetAll')
      .subscribe(res => this.cities = res);
  }

  addCity() {
    if (!this.newCityName || this.newCityCountryId == null)
      return alert('City name and country required');

    const city: CityAddUpdate = {
      name: this.newCityName,
      country_id: this.newCityCountryId
    };

    this.httpClient.post<void>(
      Config.api + '/City/Add',
      city
    ).subscribe(() => {
      this.newCityName = '';
      this.newCityCountryId = null;
      this.getCities();
    });
  }

  startEditCity(city: City) {
    this.editCityId = city.id;
    this.editCityName = city.name;
    this.editCityCountryId = city.country_id;
  }

  updateCity() {
    if (
      this.editCityId == null ||
      !this.editCityName ||
      this.editCityCountryId == null
    ) return;

    const update: CityAddUpdate = {
      name: this.editCityName,
      country_id: this.editCityCountryId
    };

    this.httpClient.put<void>(
      Config.api + `/City/Update/id?id=${this.editCityId}`,
      update
    ).subscribe(() => {
      this.cancelEditCity();
      this.getCities();
      this.getLocations();
    });
  }

  deleteCity(id: number) {
    if (!confirm('Delete this city?')) return;

    this.httpClient.delete<void>(
      Config.api + `/City/Delete/id?id=${id}`
    ).subscribe(() => {
      this.getCities();
      this.getLocations();
    });
  }

  cancelEditCity() {
    this.editCityId = null;
    this.editCityName = '';
    this.editCityCountryId = null;
  }

  getLocations() {
    this.httpClient.get<Location[]>(Config.api + '/Location/GetAll')
      .subscribe(res => this.locations = res);
  }

  addLocation() {
    if (
      !this.newLocationName ||
      this.newLocationLatitude == null ||
      this.newLocationLongitude == null ||
      this.newLocationCityId == null
    ) return alert('All location fields required');

    const loc: LocationAddUpdate = {
      name: this.newLocationName,
      latitude: this.newLocationLatitude,
      longitude: this.newLocationLongitude,
      city_id: this.newLocationCityId
    };

    this.httpClient.post<void>(
      Config.api + '/Location/Add',
      loc
    ).subscribe(() => {
      this.newLocationName = '';
      this.newLocationLatitude = null;
      this.newLocationLongitude = null;
      this.newLocationCityId = null;
      this.getLocations();
    });
  }

  startEditLocation(l: Location) {
    this.editLocationId = l.id;
    this.editLocationName = l.name;
    this.editLocationLatitude = l.latitude;
    this.editLocationLongitude = l.longitude;
    this.editLocationCityId = l.city_id;
  }

  updateLocation() {
    if (
      this.editLocationId == null ||
      !this.editLocationName ||
      this.editLocationLatitude == null ||
      this.editLocationLongitude == null ||
      this.editLocationCityId == null
    ) return;

    const update: LocationAddUpdate = {
      name: this.editLocationName,
      latitude: this.editLocationLatitude,
      longitude: this.editLocationLongitude,
      city_id: this.editLocationCityId
    };

    this.httpClient.put<void>(
      Config.api + `/Location/Update/id?id=${this.editLocationId}`,
      update
    ).subscribe(() => {
      this.cancelEditLocation();
      this.getLocations();
    });
  }

  deleteLocation(id: number) {
    if (!confirm('Delete this location?')) return;

    this.httpClient.delete<void>(
      Config.api + `/Location/Delete/id?id=${id}`
    ).subscribe(() => this.getLocations());
  }

  cancelEditLocation() {
    this.editLocationId = null;
    this.editLocationName = '';
    this.editLocationLatitude = null;
    this.editLocationLongitude = null;
    this.editLocationCityId = null;
  }
}
