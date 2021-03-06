import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Location } from 'src/model/location.model';

@Injectable({
    providedIn: 'root'
})

export class LocationService {
    constructor(private http: HttpClient) { }

    getLocations(): Observable<Location[]> {
        return this.http.get<Location[]>(`${environment.apiURL}/api/locations`);
    }

    getLocation(id: number): Observable<Location> {
        return this.http.get<Location>(`${environment.apiURL}/api/locations/${id}`);
    }
}