import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public strategy = 'fade';
  public myimages = ['assets/Images/reception.jpg','assets/Images/bedroom.jpg','assets/Images/food.jpg', 'assets/Images/hotel.jpg'];

}
