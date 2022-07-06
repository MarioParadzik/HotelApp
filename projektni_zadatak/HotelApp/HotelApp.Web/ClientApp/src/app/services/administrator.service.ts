import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from 'src/model/user.model';

@Injectable({ providedIn: 'root' })

export class AdministratorService {
  constructor(private http: HttpClient) { }

  getAdmins(): Observable<User[]> {
    return this.http.get<User[]>(`${environment.apiURL}/api/administrator`);
  }

  deleteAdmin(id: number) {
    return this.http.delete(`${environment.apiURL}/api/administrator/${id}`);
  }

  createAdmin(firstName: string, lastName: string, email: string, password: string) {
    return this.http.post(`${environment.apiURL}/api/administrator`, {firstName, lastName, email, password});
  }
}
