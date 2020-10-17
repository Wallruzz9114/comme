import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { environment } from './../environments/environment';
import { AccountService } from './account/account.service';
import { CartService } from './cart/cart.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  constructor(private cartService: CartService, private accountService: AccountService) {}

  public ngOnInit(): void {
    this.getCart();
    this.getUser();
  }

  public getCart(): void {
    const cartId = localStorage.getItem(environment.cartId);

    if (cartId) {
      this.cartService.getCart(cartId).subscribe(
        () => console.log('Initialized cart'),
        (error: HttpErrorResponse) => console.log(error)
      );
    }
  }

  public getUser(): void {
    const token = localStorage.getItem(environment.token);
    this.accountService.loadCurrentUser(token).subscribe(
      () => console.log('Loaded user'),
      (error: HttpErrorResponse) => console.log(error)
    );
  }
}
