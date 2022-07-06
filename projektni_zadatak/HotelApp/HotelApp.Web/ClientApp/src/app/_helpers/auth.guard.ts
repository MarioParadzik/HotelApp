import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { map } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.authService.currentUser.pipe(map( x => {

      if(!x || (route.data['roles'] && x.role.indexOf(route.data['roles'][0]) === -1)) {
        this.router.navigate(['/']);
        return false;
      }
       return true;
    }));
  }
}
