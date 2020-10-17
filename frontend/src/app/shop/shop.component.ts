import { HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

import { PaginatedProducts } from './../models/paginated-products';
import { Product } from './../models/product';
import { ProductBrand } from './../models/product-brand';
import { ProductType } from './../models/product-type';
import { SearchParameters } from './../models/search-parameters';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm: ElementRef;

  public products: Product[];
  public productBrands: ProductBrand[];
  public productTypes: ProductType[];
  public searchParameters = new SearchParameters();
  public totalItemsCount: number;
  public sortTypes = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getAllData();
  }

  public getAllData(): void {
    this.getProducts();
    this.getProductBrands();
    this.getProductTypes();
  }

  public getProducts(): void {
    this.shopService.getProducts(this.searchParameters).subscribe(
      (paginatedProducts: PaginatedProducts) => {
        this.products = paginatedProducts.data;
        this.searchParameters.pageIndex = paginatedProducts.pageIndex;
        this.searchParameters.pageSize = paginatedProducts.pageSize;
        this.totalItemsCount = paginatedProducts.count;
      },
      (error: HttpErrorResponse) => console.log(error)
    );
  }

  public getProductBrands(): void {
    this.shopService.getProductBrands().subscribe(
      (productBrands: ProductBrand[]) =>
        (this.productBrands = [{ id: 0, name: 'All' }, ...productBrands]),
      (error: HttpErrorResponse) => console.log(error)
    );
  }

  public getProductTypes(): void {
    this.shopService.getProductTypes().subscribe(
      (productTypes: ProductType[]) =>
        (this.productTypes = [{ id: 0, name: 'All' }, ...productTypes]),
      (error: HttpErrorResponse) => console.log(error)
    );
  }

  public selectProductBrand(brandId: number): void {
    this.searchParameters.brandId = brandId;
    this.searchParameters.pageIndex = 1;
    this.getProducts();
  }

  public selectProductType(typeId: number): void {
    this.searchParameters.typeId = typeId;
    this.searchParameters.pageIndex = 1;
    this.getProducts();
  }

  public seletSortType(sortType: string): void {
    this.searchParameters.sortType = sortType;
    this.getProducts();
  }

  public setPageNumber(pageNumber: number): void {
    if (this.searchParameters.pageIndex !== pageNumber) {
      this.searchParameters.pageIndex = pageNumber;
      this.getProducts();
    }
  }

  public inputSearch(): void {
    this.searchParameters.search = this.searchTerm.nativeElement.value;
    this.searchParameters.pageIndex = 1;
    this.getProducts();
  }

  public resetSearch(): void {
    this.searchTerm.nativeElement.value = '';
    this.searchParameters = new SearchParameters();
    this.getProducts();
  }
}
