import { Country } from "./Country.model";

export interface City{
    id: number;
    name: string;
    country_id: number;
    country: Country;
  }

export interface CityAddUpdate{
  name: string;
  country_id: number;
}