import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { AccountService } from './../../account/account.service';
import { CartService } from './../../cart/cart.service';
import { Cart } from './../../models/cart';
import { User } from './../../models/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  public cart$: Observable<Cart>;
  public currentUser$: Observable<User>;

  constructor(private cartService: CartService, private accountService: AccountService) {}

  ngOnInit(): void {
    this.cart$ = this.cartService.cart$;
    this.currentUser$ = this.accountService.currentUser$;
  }

  public logout(): void {
    this.accountService.logout();
  }
}
