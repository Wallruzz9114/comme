import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { PaginatedProducts } from './models/paginated-products';
import { Product } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  public title = 'Comme';
  public products: Product[];

  constructor(private httpClient: HttpClient) {}

  public ngOnInit(): void {
    this.httpClient.get('https://localhost:5001/api/products?pageSize=50').subscribe(
      (response: PaginatedProducts) => (this.products = response.data),
      (error) => console.log(error)
    );
  }
}
