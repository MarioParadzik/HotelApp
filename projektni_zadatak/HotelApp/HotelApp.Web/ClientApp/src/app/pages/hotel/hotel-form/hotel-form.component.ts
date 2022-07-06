import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LocationService } from 'src/app/services/location.service';
import { Hotel } from 'src/model/hotel.model';

@Component({
  selector: 'app-hotel-form',
  templateUrl: './hotel-form.component.html',
  styleUrls: ['./hotel-form.component.css']
})
export class HotelFormComponent implements OnInit {
  validateForm!: FormGroup;
  locations$ = this.locationService.getLocations();
  hotel: Hotel | null = null;
  
  constructor(private fb: FormBuilder,
              private locationService: LocationService) {}

  @Input() set hotelData(hotelInfo: Hotel){
    this.hotel = hotelInfo;
  }

  @Output() myValueSubmited = new EventEmitter<Hotel>();

  submitForm(): void {
    if (this.validateForm.valid) {
      this.hotel = new Hotel;
      this.hotel.name = this.formsParameters['name'].value,
      this.hotel.contactMail = this.formsParameters['contactMail'].value,
      this.hotel.contactNumber = this.formsParameters['contactNumber'].value,
      this.hotel.adress = this.formsParameters['adress'].value,
      this.hotel.locationId = this.formsParameters['locationId'].value == null ? this.hotel.locationId : this.formsParameters['locationId'].value,
      this.hotel.hotelStatusId =  this.formsParameters['hotelStatusId'].value
      this.myValueSubmited.emit(this.hotel);
    } else {
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }



  get formsParameters() { return this.validateForm.controls; }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      name: [null, [Validators.required]],
      contactMail: [null, [Validators.email, Validators.required]],
      contactNumber: [null, [Validators.required]],
      adress: [null, [Validators.required]],
      locationId: [this.hotel?.locationId, [Validators.required]],
      hotelStatusId:[1, [Validators.required]]
    });
  }
}
