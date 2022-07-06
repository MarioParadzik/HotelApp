import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, combineLatest, mergeMap, Observable} from 'rxjs';
import { AlertService } from 'src/app/services/alert.service';
import { HotelService } from 'src/app/services/hotel.service';
import { ReservationService } from 'src/app/services/reservation.service';
import { PagedList } from 'src/model/pagedList.model';
import { Reservation } from 'src/model/reservation.model';
import { ReservationStatuses } from 'src/model/reservationStatus';

@Component({
  selector: 'app-hotel-reservations',
  templateUrl: './hotel-reservations.component.html',
  styleUrls: ['./hotel-reservations.component.css']
})
export class HotelReservationsComponent implements OnInit {
  reservationStatuses$ = this.reservationService.getReservationStatuses();
  hotels$ = this.hotelService.getManagersHotels();
  reservations$!: Observable<PagedList<Reservation>>;
  private reservationStatusChangedSubject = new BehaviorSubject<number[]>([1]);
  reservationStatusChangedSubject$ = this.reservationStatusChangedSubject.asObservable();

  private sortChangedSubject = new BehaviorSubject<string>("asc");
  sortChangedSubject$ = this.sortChangedSubject.asObservable();

  private pageIndexChangedSubject = new BehaviorSubject<number>(1);
  pageIndexChangedSubject$ = this.pageIndexChangedSubject.asObservable();

  selectedStatuses = [1];
  selectedSort = "asc";



  constructor(private reservationService: ReservationService,
              private hotelService: HotelService,
              private alertService: AlertService) { }

  ngOnInit(): void {
    this.reservations$ = combineLatest([
      this.pageIndexChangedSubject$,
      this.reservationStatusChangedSubject$,
      this.sortChangedSubject$,
      this.hotels$])
    .pipe(
    mergeMap(([pageNumber, stauses, sort, hotels]) => this.reservationService.getReservations(pageNumber, stauses, sort, hotels)));
  }

  convertDate(d: Date): any {
    var date = new Date(d);
    return date.getDate() + '-' + (date.getMonth() + 1) + '-' + date.getFullYear();
  }

  accept(id: number) {
    this.reservationService.updateReservationStatus(id, ReservationStatuses.Accepted).subscribe(
      data => {
        this.alertService.success('Reservation accepted!');
        this.ngOnInit();
      },
      err => {
        this.alertService.error(err.error.error);
      });
  }

  reject(id: number) {
    this.reservationService.updateReservationStatus(id, ReservationStatuses.Rejected).subscribe(
      data => {
        this.alertService.success('Reservation rejected!');
        this.ngOnInit();
      },
      err => {
        this.alertService.error(err.error.error);
      });
  }

  statusSelecotor(i:any){
    this.reservationStatusChangedSubject.next(i);
  }

  sortSelecotr(i: string){
    this.sortChangedSubject.next(i);
  }

  switchPage(i: number) {
    this.pageIndexChangedSubject.next(i);
  }
}
