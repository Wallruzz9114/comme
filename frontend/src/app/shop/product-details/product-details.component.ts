import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

import { Product } from './../../models/product';
import { ShopService } from './../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  public product: Product;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private breadcrumbService: BreadcrumbService
  ) {}

  ngOnInit(): void {
    this.breadcrumbService.set('@productDetails', ' ');
    this.getProduct(+this.activatedRoute.snapshot.paramMap.get('id'));
  }

  public getProduct(id: number): void {
    this.shopService.getProduct(id).subscribe(
      (product: Product) => {
        this.product = product;
        this.breadcrumbService.set('@productDetails', product.name);
      },
      (error: any) => console.log(error)
    );
  }
}
