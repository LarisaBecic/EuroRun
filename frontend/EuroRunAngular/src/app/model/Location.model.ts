import { City } from "./City.model";

export interface Location {
  id: number;
  name: string;
  latitude: number;
  longitude: number;
  city_id: number;
  city: City;
}

export interface LocationAddUpdate {
  name: string;
  latitude: number;
  longitude: number;
  city_id: number;
}