import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { PagedList } from "src/model/pagedList.model";
import { Reservation } from "src/model/reservation.model";
import { ReservationStatus } from "src/model/reservationStatus";

@Injectable({
    providedIn: 'root'
  })

export class ReservationService {

    constructor(private http: HttpClient) { }
    params: HttpParams = new HttpParams();

    paramsInitializer(pageNumber: number, reservationStatusIds: number[], sort: string, hotelIds: number[]){
        this.params = this.params.set('PageNumber', pageNumber);
        if (reservationStatusIds.length > 0) {
            reservationStatusIds.forEach(el => {
              this.params = this.params.append('ReservationStatusIds', el)
            });
        }

        if (hotelIds.length > 0) {
            hotelIds.forEach(el => {
              this.params = this.params.append('HotelIds', el)
            });
        }

        if(sort != null) {
            this.params = this.params.append('OrderBy', sort == "asc" ? "DateCreated" : "DateCreated desc");
        }
    }

    getReservations(pageNumber: number, reservationStatusIds: any[], sort:string, hotelIds: any[]): Observable<PagedList<Reservation>> {
        this.paramsInitializer(pageNumber, reservationStatusIds, sort, hotelIds);
        var paramKeeper = this.params;
        this.params =  new HttpParams();
        return this.http.get<any>(`${environment.apiURL}/api/reservations`, { params: paramKeeper });
    }

    createReservation(roomId: number, dateFrom: Date, dateTo: Date, note: string) {
        return this.http.post(`${environment.apiURL}/api/rooms/${roomId}/reservations`, {dateFrom, dateTo, note, roomId });
    }
    getUserReservations(): Observable<Reservation[]> {
        return this.http.get<Reservation[]>(`${environment.apiURL}/api/reservations/user`);
    }

    getReservationStatuses(): Observable<ReservationStatus[]> {
        return this.http.get<ReservationStatus[]>(`${environment.apiURL}/api/reservations/status`);
    }
    
    updateReservationStatus(id: number, statusId: number) {
        return this.http.patch(`${environment.apiURL}/api/reservations/${id}/status`, {statusId});
    }

    syncExternalServices(){
        return this.http.get(`${environment.apiURL}/api/reservations/external`);
    }

}