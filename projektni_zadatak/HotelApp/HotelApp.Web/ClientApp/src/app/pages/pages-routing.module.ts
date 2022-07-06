import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Role } from 'src/model/role.model';
import { AuthGuard } from '../_helpers/auth.guard';
import { AdminListComponent } from './admin/admin-list/admin-list.component';
import { ConfigurationsComponent } from './admin/configurations/configurations.component';
import { HotelsComponent } from './admin/hotels/hotels.component';
import { HomeComponent } from './home/home.component';
import { HotelDetailsComponent } from './hotel/hotel-details/hotel-details.component';
import { HotelReservationsComponent } from './hotel/hotel-reservations/hotel-reservations.component';
import { MyHotelListComponent } from './hotel/my-hotel-list/my-hotel-list.component';
import { RegisterHotelComponent } from './hotel/register-hotel/register-hotel.component';
import { ReservationsComponent } from './room/reservations/reservations.component';
import { RoomDetailsComponent } from './room/room-details/room-details.component';
import { RoomsComponent } from './room/rooms/rooms.component';

const routes: Routes = [
    {path: 'home', component: HomeComponent},
    {path: 'rooms', component:RoomsComponent },
    {path: 'rooms/:id/reservation', component:RoomDetailsComponent},
    {path: 'reservations', component:ReservationsComponent, canActivate: [AuthGuard], data: { roles: [Role.RegisteredUser, Role.HotelManager] }},
    {path: 'hotel/reservations', component:HotelReservationsComponent, canActivate: [AuthGuard], data: { roles: [Role.HotelManager] }},
    {path: 'configurations', component:ConfigurationsComponent, canActivate: [AuthGuard], data: { roles: [Role.SuperAdministrator] }},
    {path: 'hotel/register', component:RegisterHotelComponent},
    {path: 'my-hotels', component:MyHotelListComponent, canActivate: [AuthGuard], data: { roles: [Role.HotelManager] }},
    {path: 'my-hotels/:id', component:HotelDetailsComponent, canActivate: [AuthGuard], data: { roles: [Role.HotelManager] }},
    {path: 'administrators', component:AdminListComponent, canActivate: [AuthGuard], data: { roles: [Role.SuperAdministrator] }},
    {path: 'hotels', component:HotelsComponent, canActivate: [AuthGuard], data: { roles: [Role.SuperAdministrator, Role.Administrator] }},
    { path: '**', redirectTo: 'home' }
    
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class PagesRoutingModule { }