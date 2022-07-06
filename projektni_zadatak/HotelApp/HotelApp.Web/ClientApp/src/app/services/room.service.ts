import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { HotelStatus } from "src/model/hotelStatus.model";
import { PagedList } from "src/model/pagedList.model";
import { Room } from "src/model/room.model";


@Injectable({
    providedIn:'root'
})

export class RoomService{
    constructor(private http: HttpClient) {}
    params: HttpParams = new HttpParams();
    
    paramsInitializer(pageNumber: number, beds: number[], locations: number[], date: Date[], sort: string){
        this.params = this.params.set('PageNumber', pageNumber);
        this.params = this.params.set('HotelStatus', HotelStatus.Active);
        if (beds.length > 0) {
            beds.forEach(el => {
              this.params = this.params.append('NumberOfBeds', el)
            });
        }
        if (locations.length > 0) {
            locations.forEach(el => {
              this.params = this.params.append('LocationIds', el)
            });
        }
        if (date.length > 1 ) {
            this.params = this.params.append('DateFrom', date[0].toISOString())
            this.params = this.params.append('DateTo', date[1].toISOString())
        }
        if(sort != null) {
            this.params = this.params.append('OrderBy', sort == "asc" ? "Price" : "Price-desc");
        }
        
    }
    
    getRooms(pageNumber: number, beds: any[], locations: any[], date: any[], sort:string): Observable<PagedList<Room>> {
        this.params =  new HttpParams();
        this.paramsInitializer(pageNumber, beds, locations, date, sort);
        return this.http.get<any>(`${environment.apiURL}/api/rooms`, { params: this.params });
    }

    getRoomById(id: number): Observable<Room> {
        return this.http.get<Room>(`${environment.apiURL}/api/rooms/${id}`);
    }

    addRoom(name: string, numberOfBeds: number, price: number, hotelId: number) {
        return this.http.post(`${environment.apiURL}/api/rooms`, { name, numberOfBeds, price, hotelId });
    }

    updateRoom(id: number, name: string, numberOfBeds: number, price: number, hotelId: number) {
        return this.http.put<Room>(`${environment.apiURL}/api/rooms/${id}`, { id, name, numberOfBeds, price, hotelId });
    }

    deleteRoom(id: number) {
        return this.http.delete(`${environment.apiURL}/api/rooms/${id}`);
    }

    getNumberOfBeds(): Observable<any[]> {
        return this.http.get<any[]>(`${environment.apiURL}/api/rooms/bedNumbers`);
    }
}