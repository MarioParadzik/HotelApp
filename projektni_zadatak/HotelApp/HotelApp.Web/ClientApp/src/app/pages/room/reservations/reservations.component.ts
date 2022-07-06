import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AlertService } from 'src/app/services/alert.service';
import { ReservationService } from 'src/app/services/reservation.service';
import { Reservation } from 'src/model/reservation.model';
import { ReservationStatuses } from 'src/model/reservationStatus';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent implements OnInit {
  reservations$: Observable<Reservation[]> | null = null;
  showExtraClass = true;
  constructor(
    private reservationService: ReservationService,
    private alertService: AlertService
  ) { }

  ngOnInit(): void {
    this.reservations$ = this.reservationService.getUserReservations();
  }

  convertDate(d: Date): any {
    var date = new Date(d);
    return date.getDate() + '-' + (date.getMonth() + 1) + '-' + date.getFullYear();
  }

  cancel() {
    this.alertService.info('Canceled.');
  }

  delete(id:number) {
    this.reservationService.updateReservationStatus(id, ReservationStatuses.Canceled).subscribe(
      data => {
        this.alertService.success('Reservation canceled!');
        this.ngOnInit();
      },
      err => {
        this.alertService.error(err.error.error);
      });
  }


}
