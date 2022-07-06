import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NzModalService } from 'ng-zorro-antd/modal';
import { BehaviorSubject, combineLatest, first, map, mergeMap, Observable, switchMap, tap } from 'rxjs';
import { AlertService } from 'src/app/services/alert.service';
import { HotelService } from 'src/app/services/hotel.service';
import { RoomService } from 'src/app/services/room.service';
import { Hotel } from 'src/model/hotel.model';
import { PagedList } from 'src/model/pagedList.model';
import { Room } from 'src/model/room.model';

@Component({
  selector: 'app-hotel-details',
  templateUrl: './hotel-details.component.html',
  styleUrls: ['./hotel-details.component.css']
})
export class HotelDetailsComponent implements OnInit {
  hotel$: Observable<Hotel> | null = null;
  hotelId!: number;
  rooms$!: Observable<PagedList<Room>>;
  isVisible = false;
  room!: Room;
  roomName!: string;

  private pageIndexChangedSubject = new BehaviorSubject<number>(1);
  pageIndexChangedSubject$ = this.pageIndexChangedSubject.asObservable();



  
  constructor(private route: ActivatedRoute,
              private hotelService: HotelService,
              private roomService: RoomService,
              private alertService: AlertService,
              private modal: NzModalService
              ) { }

  ngOnInit(): void {
    this.hotelId = +this.route.snapshot.params['id']
    this.hotel$ =  this.hotelService.getHotelById(this.hotelId);
    this.rooms$ = combineLatest([this.pageIndexChangedSubject$])
    .pipe(
    mergeMap(([pageNumber]) => this.hotelService.getHotelRooms(this.hotelId, pageNumber)));

  }
  
  switchPage(i: number) {
    this.pageIndexChangedSubject.next(i);
  }

  confirm(id: number){
    this.roomService.deleteRoom(id).subscribe(
      data => {
        this.alertService.success('Room deleted!');
        this.ngOnInit();
      },
      err => {
        this.alertService.error(err.error.error);
      });
  }

  showModal(): void {
    this.isVisible = true;
  }

  CancleAdd(){
    this.isVisible = false;
  }

  addRoom(room: Room){
    this.roomService.addRoom(
      room.name,
      room.numberOfBeds,
      room.price,
      this.hotelId
    )
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Added Room!');
          this.isVisible = false;
          this.ngOnInit();
        },
        err => {
          this.alertService.error(err.error.error);
          this.isVisible = false;
        });
  }

  updateRoom(room: Room){
    this.roomService.updateRoom(
      room.id,
      room.name,
      room.numberOfBeds,
      room.price,
      this.hotelId
    )
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Updated Room!');
          this.modal.ngOnDestroy();
          this.ngOnInit();
        },
        err => {
          this.alertService.error(err.error.error);
        });
  }

  updateHotel(hotel: Hotel){
    this.hotelService.updateHotel(
      this.hotelId,
      hotel.name,
      hotel.contactMail,
      hotel.contactNumber,
      hotel.adress,
      hotel.locationId,
      hotel.hotelStatusId
    ).pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Updated Hotel!');
          this.ngOnInit();
        },
        err => {
          this.alertService.error(err.error.error);
        });
  }
  
  showModalTop(id: number, tplTitle: TemplateRef<{}>, tplContent: TemplateRef<{}>): void {
    this.roomService.getRoomById(id).subscribe( r => {this.room = r;
      this.roomName = r.name;
      this.modal.confirm({
        nzTitle: tplTitle,
        nzContent: tplContent,
        nzCancelText: null,
        nzOkText: null,
        nzFooter: null,
        nzStyle: {top: `10px`},
        nzWidth: '500px'
      });
    });
    

  }

}