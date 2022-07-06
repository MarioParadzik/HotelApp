import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { environment } from "src/environments/environment";
import { Configuration } from "src/model/configuration.model";

@Injectable({
    providedIn: 'root'
})

export class ConfigurationService {
    constructor(private http: HttpClient) { }

    getConfigurations(): Observable<Configuration[]> {
        return this.http.get<Configuration[]>(`${environment.apiURL}/api/configurations`);
    }

    addConfiguration(key: string, value: number, type: string, description: string) {
        return this.http.post<Configuration>(`${environment.apiURL}/api/configurations`, { key, value, type, description });
    }

    updateConfiguration(id: number, value: string) {
        return this.http.patch(`${environment.apiURL}/api/configurations/${id}`, {value});
    }

    deleteConfiguration(id: number) {
        return this.http.delete(`${environment.apiURL}/api/configurations/${id}`);
    }
}