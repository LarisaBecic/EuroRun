import { EventType } from "./EventType.model";
import { Location } from "./Location.model";

export interface Event {
  id: number;
  name: string;
  dateTime: string;
  description: string;
  registrationDeadline: string;
  eventType_id: number;
  eventType: EventType;
  location_id: number;
  location: Location;
  picture?: string;
  userFavourite?: boolean;
  favouritedTimes: number;
}

export interface EventAddUpdate {
  name: string;
  eventType_id: number;
  location_id: number;
  dateTime: string;
  description: string;
  registrationDeadline: string;
  picture?: string;
}