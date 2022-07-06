import { Observable } from "rxjs";

export class Reservation {
  id!: number;
  dateCreated!: Date;
  dateFrom!: Date;
  dateTo!: Date;
  note?: string;
  roomName!: string;
  hotelName!: string;
  reservationStatusId!: number;
  reservationStatus!: string;
  canCancel!: boolean;
}
