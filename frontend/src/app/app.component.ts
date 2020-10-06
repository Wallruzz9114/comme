import { Component, OnInit } from '@angular/core';

import { environment } from './../environments/environment';
import { CartService } from './cart/cart.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  constructor(private cartService: CartService) {}

  public ngOnInit(): void {
    const cartId = localStorage.getItem(environment.cartId);

    if (cartId) {
      this.cartService.getCart(cartId).subscribe(
        () => console.log('Initialized cart'),
        (error) => console.log(error)
      );
    }
  }
}
