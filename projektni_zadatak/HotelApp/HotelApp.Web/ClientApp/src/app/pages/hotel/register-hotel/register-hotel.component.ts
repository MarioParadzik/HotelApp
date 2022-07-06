import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs';
import { AlertService } from 'src/app/services/alert.service';
import { HotelService } from 'src/app/services/hotel.service';
import { Hotel } from 'src/model/hotel.model';
import { Role } from 'src/model/role.model';
import { User } from 'src/model/user.model';

@Component({
  selector: 'app-register-hotel',
  templateUrl: './register-hotel.component.html',
  styleUrls: ['./register-hotel.component.css']
})
export class RegisterHotelComponent implements OnInit {

  constructor(private hotelService: HotelService,
              private alertService: AlertService,
              private router: Router) { }
  
  ngOnInit(): void {
  }

  registerHotel(hotel: Hotel){
    debugger
    this.hotelService.registerHotel(
      hotel.name,
      hotel.contactMail,
      hotel.contactNumber,
      hotel.adress,
      hotel.locationId
    ).pipe(first())
      .subscribe(
        data => {
          debugger
          let user: User | null = null;
          const localStorageData = localStorage.getItem('currentUser');
          if(localStorageData) {
            user = JSON.parse(localStorageData);
            if(user !== null && user.role !== Role.HotelManager) {
              user.role = Role.HotelManager;
              localStorage.setItem('currentUser', JSON.stringify(user));
              localStorage.setItem('userRole', user.role)
            }
          }
          this.alertService.success('Created new Hotel!');
          window.location.replace('/');
        },
        err => {
          this.alertService.error(err.error.error);
        });

        this.router.navigate(['/my-hotels']);
  }

}
