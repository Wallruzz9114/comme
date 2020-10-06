import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { CartService } from './../../../cart/cart.service';
import { CartSummary } from './../../../models/cart-summary';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss'],
})
export class OrderTotalsComponent implements OnInit {
  public cartSummary$: Observable<CartSummary>;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.cartSummary$ = this.cartService.cartSummary$;
  }
}
