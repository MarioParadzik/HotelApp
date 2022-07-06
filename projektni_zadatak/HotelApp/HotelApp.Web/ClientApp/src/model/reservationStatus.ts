export class ReservationStatus {
  id!: number;
  name!: string;
}
  
export enum ReservationStatuses {
  InProcess = 1,
  Accepted = 2,
  Rejected = 3,
  Canceled = 4
}
