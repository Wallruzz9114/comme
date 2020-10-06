import { Component, Input, OnInit } from '@angular/core';

import { CartService } from './../../cart/cart.service';
import { Product } from './../../models/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss'],
})
export class ProductItemComponent implements OnInit {
  @Input() product: Product;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {}

  public addItemToCart(): void {
    this.cartService.addItemToCart(this.product);
  }
}
