import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Product } from './../../models/product';
import { ShopService } from './../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  public product: Product;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.getProduct(+this.activatedRoute.snapshot.paramMap.get('id'));
  }

  public getProduct(id: number): void {
    this.shopService.getProduct(id).subscribe(
      (product: Product) => (this.product = product),
      (error: any) => console.log(error)
    );
  }
}
