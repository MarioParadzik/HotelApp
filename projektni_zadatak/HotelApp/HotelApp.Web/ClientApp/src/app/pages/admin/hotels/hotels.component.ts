import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AlertService } from 'src/app/services/alert.service';
import { HotelService } from 'src/app/services/hotel.service';
import { Hotel } from 'src/model/hotel.model';
import { HotelStatus } from 'src/model/hotelStatus.model';

@Component({
  selector: 'app-hotels',
  templateUrl: './hotels.component.html',
  styleUrls: ['./hotels.component.css']
})
export class HotelsComponent implements OnInit {
  hotelList$!: Observable<Hotel[]>;

  constructor(private hotelService: HotelService,
              private alertService: AlertService) { }

  ngOnInit(): void {
    this.hotelList$ = this.hotelService.getUnconfirmedHotels();
  }

  active(id: number){
    this.hotelService.updateHotelStatus(id, HotelStatus.Active).subscribe(
      data => {
        this.alertService.success('Hotel activated!');
        this.ngOnInit();
      },
      err => {
        this.alertService.error(err.error.error);
      });
  }

  inactive(id: number){
    this.hotelService.updateHotelStatus(id, HotelStatus.Inactive).subscribe(
      data => {
        this.alertService.success('Hotel canceled!');
        this.ngOnInit();
      },
      err => {
        this.alertService.error(err.error.error);
      });
  }
}
