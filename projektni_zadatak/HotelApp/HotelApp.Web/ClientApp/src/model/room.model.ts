import { Observable } from "rxjs";

export class Room {
  id!: number;
  name!: string;
  numberOfBeds!: number;
  price!: number;
  hotelId!: number;
  hotelName?: Observable<string>;
  locationName?: Observable<string>;
  contactMail?: Observable<string>;
  contactNumber?: Observable<string>;
  adress?: Observable<string>;
}
