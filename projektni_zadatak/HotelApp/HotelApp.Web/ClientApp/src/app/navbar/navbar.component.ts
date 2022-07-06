import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  condition = true;
  isVisible = false;
  authenticated!: boolean

  constructor(public authService: AuthService,
              private router: Router) { }

  ngOnInit(): void {
    this.authService.isAuthenticated$.subscribe(x => this.authenticated = x)
  }
  showModal(): void {
    if(!this.authenticated){
      this.isVisible = true;
    } else {
      this.router.navigate([`/hotel/register`]);
    }
   
  }

  login(){
    this.router.navigate([`../identity/login`], {state: {returnUrl: this.router.url}} );
  }

  handleCancel() {
    this.isVisible = false;
  }

  routeToLogin(){
    this.router.navigate([`../identity/login`], {state: {returnUrl:'/hotel/register'}});
    this.handleCancel();
  }

  routeToRegister(){
    this.router.navigate([`../identity/register`], {state: {returnUrl:'/hotel/register'}});
    this.handleCancel();
  }
}
