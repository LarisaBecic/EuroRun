import { EventType } from "./EventType.model";
import { Location } from "./location.model";


export interface Event{
  id: number;
  name: string;
  dateTime: string;
  description: string;
  registrationDeadline: string;
  eventType_id: number;
  eventType: EventType;
  location_id: number;
  location: Location;
  }