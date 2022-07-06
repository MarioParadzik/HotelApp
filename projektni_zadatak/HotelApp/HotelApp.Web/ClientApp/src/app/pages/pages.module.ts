import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PagesRoutingModule } from './pages-routing.module';
import { HomeComponent } from './home/home.component';
import { NzCarouselModule } from 'ng-zorro-antd/carousel';
import { AdminModule } from './admin/admin.module';
import { RoomModule } from './room/room.module';
import { HotelModule } from './hotel/hotel-module.module';

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    PagesRoutingModule,
    NzCarouselModule,
    AdminModule,
    RoomModule,
    HotelModule
  ]
})
export class PagesModule { }
