import { City } from "./city.model";

export interface Location{
  id: number;
  name: string;
  latitude: number;
  longitude: number;
  city_id: number;
  city:City;
  }