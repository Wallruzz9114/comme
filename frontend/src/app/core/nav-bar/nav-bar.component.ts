import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { CartService } from './../../cart/cart.service';
import { Cart } from './../../models/cart';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  public cart$: Observable<Cart>;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.cart$ = this.cartService.cart$;
  }
}
