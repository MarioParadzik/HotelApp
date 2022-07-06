import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfigurationsComponent } from './configurations/configurations.component';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzInputModule } from 'ng-zorro-antd/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { AdminListComponent } from './admin-list/admin-list.component';
import { HotelsComponent } from './hotels/hotels.component';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';



@NgModule({
  declarations: [ConfigurationsComponent, AdminListComponent, HotelsComponent],
  imports: [
    CommonModule,
    FormsModule,
    NzFormModule,
    NzModalModule,
    NzTableModule,
    NzInputModule,
    ReactiveFormsModule,
    NzButtonModule,
    NzPaginationModule,
    NzIconModule,
    NzListModule,
    NzPopconfirmModule,
    NzCardModule,
    NzSkeletonModule,
    NzAvatarModule,
    NzGridModule,
    NzToolTipModule
  ]
})
export class AdminModule { }
