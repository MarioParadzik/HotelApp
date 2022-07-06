import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatTooltipModule} from '@angular/material/tooltip';
import { RoomsComponent } from './rooms/rooms.component';
import { RoomDetailsComponent } from './room-details/room-details.component';
import { ReservationsComponent } from './reservations/reservations.component';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { FormsModule } from '@angular/forms';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';

@NgModule({
  declarations: [
    RoomsComponent,
    RoomDetailsComponent,
    ReservationsComponent
  ],
  imports: [
    CommonModule,
    MatTooltipModule,
    NzTableModule,
    NzIconModule,
    NzPopconfirmModule,
    NzCardModule,
    NzDatePickerModule,
    NzInputModule,
    NzGridModule,
    FormsModule,
    NzSelectModule,
    NzPaginationModule 
  ]
})
export class RoomModule { }
