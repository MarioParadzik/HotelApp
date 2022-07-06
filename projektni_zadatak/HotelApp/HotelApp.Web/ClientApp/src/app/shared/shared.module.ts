import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InRoleDirective } from './directive/not-in-role.directive';
import { NzAlertModule } from 'ng-zorro-antd/alert';
import { NzMessageModule } from 'ng-zorro-antd/message';
import { NzIconModule } from 'ng-zorro-antd/icon';


@NgModule({
  declarations: [
    InRoleDirective,
    
  ],
  imports: [
    CommonModule
  ],
  exports: [
    InRoleDirective,
    NzMessageModule,
    NzAlertModule,
    NzIconModule
    ]
})
export class SharedModule { }
