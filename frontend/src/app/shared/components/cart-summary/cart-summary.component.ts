import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';

import { CartService } from '../../../cart/cart.service';
import { Cart } from '../../../models/cart';
import { CartItem } from '../../../models/cart-item';

@Component({
  selector: 'app-cart-summary',
  templateUrl: './cart-summary.component.html',
  styleUrls: ['./cart-summary.component.scss'],
})
export class CartSummaryComponent implements OnInit {
  public cart$: Observable<Cart>;
  @Output() decrement: EventEmitter<CartItem> = new EventEmitter<CartItem>();
  @Output() increment: EventEmitter<CartItem> = new EventEmitter<CartItem>();
  @Output() remove: EventEmitter<CartItem> = new EventEmitter<CartItem>();
  @Input() isCheckoutPage = false;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.cart$ = this.cartService.cart$;
  }

  public incrementItemQuantity(cartItem: CartItem): void {
    this.increment.emit(cartItem);
  }

  public decrementItemQuantity(cartItem: CartItem): void {
    this.decrement.emit(cartItem);
  }

  public removeItem(cartItem: CartItem): void {
    this.remove.emit(cartItem);
  }
}
