import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subscription, switchMap, tap } from 'rxjs';
import { AlertService } from 'src/app/services/alert.service';
import { AuthService } from 'src/app/services/auth.service';
import { ReservationService } from 'src/app/services/reservation.service';
import { RoomService } from 'src/app/services/room.service';
import { Room } from 'src/model/room.model';

@Component({
  selector: 'app-room-details',
  templateUrl: './room-details.component.html',
  styleUrls: ['./room-details.component.css']
})
export class RoomDetailsComponent implements OnInit, OnDestroy {
  room$: Observable<Room> | null = null;
  roomId!: number;
  dateFrom!: Date;
  dateTo!: Date;
  defaultDate!: Date[];
  inputValue: string = "";
  subscriptions: Subscription[] = [];

  constructor(private roomService: RoomService,
              private route: ActivatedRoute,
              private reservationService: ReservationService,
              private alertService: AlertService,
              public authService: AuthService) {
  }

  ngOnInit(): void {
      this.room$ = this.route.params.pipe(tap(roomId =>  this.roomId = +roomId['id']), switchMap( params => this.roomService.getRoomById(+params['id'])));
      this.subscriptions.push(this.route.queryParams.subscribe( res => {
        this.dateFrom = res['dateFrom'] ? res['dateFrom'] : null;
        this.dateTo = res['dateTo'] ? res['dateTo'] : null;
      }))
      if(this.dateFrom != null && this.dateTo != null) {
        this.defaultDate = [this.dateFrom, this.dateTo]
      }
  }

  ngOnDestroy(){
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  disabledStartDate = (startValue: Date): boolean => {
    if (!startValue) {
      return false;
    }
    return startValue.getTime() < new Date().getTime();
  };

  makeReservation(){
    let startDate =  new Date(Date.parse(this.defaultDate[0].toString()));
    let endDate =   new Date(Date.parse(this.defaultDate[1].toString()));
    this.reservationService.createReservation(
      this.roomId,
      startDate,
      endDate,
      this.inputValue
    ).subscribe(
      data => {
        this.alertService.success('Reservation created!');
        this.defaultDate = [];
        this.inputValue = "";
      },
      err => {
        debugger
        this.alertService.error(err.error.error);
      });
  }
}
