import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { CartItem } from '../../../models/cart-item';
import { OrderItem } from './../../../models/order-item';

@Component({
  selector: 'app-cart-summary',
  templateUrl: './cart-summary.component.html',
  styleUrls: ['./cart-summary.component.scss'],
})
export class CartSummaryComponent implements OnInit {
  @Output() decrement: EventEmitter<CartItem> = new EventEmitter<CartItem>();
  @Output() increment: EventEmitter<CartItem> = new EventEmitter<CartItem>();
  @Output() remove: EventEmitter<CartItem> = new EventEmitter<CartItem>();
  @Input() isCheckoutPage = false;
  @Input() cartItems: CartItem[] | OrderItem[] = [];
  @Input() isOrder = false;

  constructor() {}

  ngOnInit(): void {}

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
