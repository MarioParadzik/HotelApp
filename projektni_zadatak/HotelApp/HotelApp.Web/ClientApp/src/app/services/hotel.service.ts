import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Hotel } from 'src/model/hotel.model';
import { HotelStatus } from 'src/model/hotelStatus.model';
import { PagedList } from 'src/model/pagedList.model';
import { Room } from 'src/model/room.model';

@Injectable({
    providedIn: 'root'
})

export class HotelService {
    constructor(private http: HttpClient) {}

    getHotelById(id: number): Observable<Hotel> {
        return this.http.get<Hotel>(`${environment.apiURL}/api/hotel/${id}`);
    }
    getHotels(): Observable<Hotel[]> {
        return this.http.get<Hotel[]>(`${environment.apiURL}/api/hotel`);
    }

    getManagersHotels(): Observable<number[]> {
        return this.http.get<number[]>(`${environment.apiURL}/api/hotel/my-hotels`);
    }

    getUnconfirmedHotels(): Observable<Hotel[]> {
        return this.http.get<Hotel[]>(`${environment.apiURL}/api/hotel/unconfirmedHotels`);
    }

    updateHotelStatus(id: number, statusId: number) {
        return this.http.patch(`${environment.apiURL}/api/hotel/${id}/status`, {statusId});
    }  

    getHotelRooms(id: number, pageNumber: number): Observable<PagedList<Room>> {
        let params = new HttpParams();
        params = params.set('PageNumber', pageNumber);
        return this.http.get<any>(`${environment.apiURL}/api/hotel/${id}/rooms`, { params: params });
    }

    updateHotel(id: number, name: string, contactMail: string, contactNumber: string,  adress: string, locationId: number, hotelStatusId: number) {
        return this.http.put<Hotel>(`${environment.apiURL}/api/hotel/${id}`, { name, contactMail, contactNumber, adress, locationId, hotelStatusId });
    }

    registerHotel(name: string, contactMail: string, contactNumber: string, adress: string, locationId: number) {
        return this.http.post<any>(`${environment.apiURL}/api/hotel`, {name, contactMail, contactNumber, adress, locationId});
      }
}