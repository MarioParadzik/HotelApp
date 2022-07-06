import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HotelFormComponent } from './hotel-form/hotel-form.component';
import { HotelDetailsComponent } from './hotel-details/hotel-details.component';
import { RegisterHotelComponent } from './register-hotel/register-hotel.component';
import { MyHotelListComponent } from './my-hotel-list/my-hotel-list.component';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzModalModule,  } from 'ng-zorro-antd/modal';
import { RoomFormComponent } from './room-form/room-form.component';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { HotelReservationsComponent } from './hotel-reservations/hotel-reservations.component';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzDividerModule } from 'ng-zorro-antd/divider';

@NgModule({
  declarations: [
    HotelFormComponent,
    HotelDetailsComponent,
    RegisterHotelComponent,
    MyHotelListComponent,
    RoomFormComponent,
    HotelReservationsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    NzFormModule,
    NzInputModule,
    ReactiveFormsModule,
    NzListModule,
    NzToolTipModule,
    NzGridModule,
    NzCardModule,
    NzAvatarModule,
    NzSkeletonModule,
    NzIconModule,
    NzPopconfirmModule,
    NzPaginationModule,
    NzModalModule,
    NzSelectModule,
    NzTableModule,
    NzDividerModule 
  ]
})
export class HotelModule { }
