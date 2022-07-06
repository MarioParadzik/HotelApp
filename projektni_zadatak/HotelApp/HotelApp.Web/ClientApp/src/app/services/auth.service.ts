import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from 'src/model/user.model';

@Injectable({
    providedIn: 'root'
})

export class AuthService {
    private currentUserSubject: BehaviorSubject<User | null>;
    public currentUser: Observable<User | null>;
    private isAuthenticated : BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
 
    constructor(private http: HttpClient) {
        let user: User | null = null;
        const localStorageData = localStorage.getItem('currentUser');
        if(localStorageData) {
            user = JSON.parse(localStorageData);
            this.isAuthenticated.next(true);
        }
        this.currentUserSubject = new BehaviorSubject<User | null>(user);
        this.currentUser = this.currentUserSubject.asObservable();
        
    }


    public isAuthenticated$ = this.isAuthenticated.asObservable();


    login(email: string, password: string) {
        return this.http.post<any>(`${environment.apiURL}/api/authentification/login`, { email, password })
          .pipe(map(user => {
                if (user && user.token) {
                    this.isAuthenticated.next(true);
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    localStorage.setItem('userName', user.name);
                    localStorage.setItem('userToken', user.token);
                    localStorage.setItem('userRole', user.role);
                }
                return user;
          }));
    }

    logout() {
        localStorage.removeItem('currentUser');
        localStorage.removeItem('userName');
        localStorage.removeItem('userToken');
        localStorage.removeItem('userRole');
        this.isAuthenticated.next(false);
        this.currentUserSubject.next(null!);
        window.location.replace('/');
    }
    
    register(firstName: string, lastName: string, email: string, password: string, confirmPassword: string) {
        var data = {
          firstName: firstName,
          lastName: lastName,
          email: email,
          password: password,
          confirmPassword: confirmPassword
        }
        return this.http.post<any>(`${environment.apiURL}/api/authentification/register`, data)
          .pipe(map(user => {
                this.isAuthenticated.next(true);
                localStorage.setItem('currentUser', JSON.stringify(user));
                localStorage.setItem('userName', user.name);
                localStorage.setItem('userToken', user.token);
                localStorage.setItem('userRole', user.role);
                this.currentUserSubject.next(user);
                return user;
          }));
      }

    getAccessToken() {
        var ls =  localStorage.getItem('userToken')
        if(!ls) return null;
        return ls;
    }

    get getCurrentUserRole() {
        var ls =  localStorage.getItem('userRole')
        return ls;
    }

    get getCurrentUserName() {
        var ls =  localStorage.getItem('userName')
        return ls;
    }
}

