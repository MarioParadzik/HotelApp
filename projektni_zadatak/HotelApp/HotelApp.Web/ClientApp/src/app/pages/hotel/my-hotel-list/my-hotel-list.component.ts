import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { HotelService } from 'src/app/services/hotel.service';
import { Hotel } from 'src/model/hotel.model';

@Component({
  selector: 'app-my-hotel-list',
  templateUrl: './my-hotel-list.component.html',
  styleUrls: ['./my-hotel-list.component.css']
})
export class MyHotelListComponent implements OnInit {
  hotelList$!: Observable<Hotel[]>;
  userName: string | null = null; 
  constructor(public authService: AuthService,
    private hotelService: HotelService,
    private router: Router) {
    this.userName = authService.getCurrentUserName;
   }

  ngOnInit(): void {
    this.hotelList$ = this.hotelService.getHotels();
  }

  hotelDetails(id: number){
    this.router.navigate(
      [`/my-hotels/${id}`]);
  }
}
