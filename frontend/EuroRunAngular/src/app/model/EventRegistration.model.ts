import { UserAccount } from "../helpers/login-info";
import { Event } from "./Event.model";

export interface EventRegistration {
  id: number;
  registrationDate: string;
  userAccount: UserAccount;
  event: Event;
  club: string;
  shirtSize: string;
  numberOfFinishedRaces: number;
  eventDiscoverySource: string;
  note: string;
}

export interface EventRegistrationAdd {
  userAccount_id: number;
  event_id: number;
  club?: string;
  shirtSize?: string;
  numberOfFinishedRaces?: number;
  eventDiscoverySource?: string;
  note?: string;
}