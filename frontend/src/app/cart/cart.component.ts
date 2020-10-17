import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BreadcrumbService } from 'xng-breadcrumb';

import { Cart } from '../models/cart';
import { CartItem } from './../models/cart-item';
import { CartTotals } from './../models/cart-totals';
import { CartService } from './cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
})
export class CartComponent implements OnInit {
  public cart$: Observable<Cart>;
  public cartSummary$: Observable<CartTotals>;
  public breadcrumb$: Observable<any[]>;

  constructor(private cartService: CartService, private breadcrumbService: BreadcrumbService) {}

  ngOnInit(): void {
    this.cart$ = this.cartService.cart$;
    this.cartSummary$ = this.cartService.cartSummary$;
    this.breadcrumb$ = this.breadcrumbService.breadcrumbs$;
  }

  public removeItem(cartItem: CartItem): void {
    this.cartService.removeItemFromCart(cartItem);
  }

  public incrementItemQuantity(cartItem: CartItem): void {
    this.cartService.incrementCartItemQuantity(cartItem);
  }

  public decrementItemQuantity(cartItem: CartItem): void {
    this.cartService.decrementCartItemQuantity(cartItem);
  }
}
