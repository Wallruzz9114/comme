import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Product } from '../models/product';
import { environment } from './../../environments/environment';
import { PaginatedProducts } from './../models/paginated-products';
import { ProductBrand } from './../models/product-brand';
import { ProductType } from './../models/product-type';
import { SearchParameters } from './../models/search-parameters';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  public baseURL = environment.apiURL;

  constructor(private httpClient: HttpClient) {}

  public getProducts(parameters: SearchParameters): Observable<PaginatedProducts> {
    let searchParameters = new HttpParams();

    if (parameters.brandId !== 0) {
      searchParameters = searchParameters.append('brandId', parameters.brandId.toString());
    }

    if (parameters.typeId !== 0) {
      searchParameters = searchParameters.append('typeId', parameters.typeId.toString());
    }

    if (parameters.search) {
      searchParameters = searchParameters.append('search', parameters.search);
    }

    searchParameters = searchParameters.append('sort', parameters.sortType);
    searchParameters = searchParameters.append('pageIndex', parameters.pageIndex.toString());
    searchParameters = searchParameters.append('pageSize', parameters.pageSize.toString());

    return this.httpClient
      .get<PaginatedProducts>(this.baseURL + 'products', {
        observe: 'response',
        params: searchParameters,
      })
      .pipe(
        map((response: HttpResponse<PaginatedProducts>) => {
          return response.body;
        })
      );
  }

  public getProduct(id: number): Observable<Product> {
    return this.httpClient.get<Product>(this.baseURL + `products/${id}`);
  }

  public getProductBrands(): Observable<ProductBrand[]> {
    return this.httpClient.get<ProductBrand[]>(this.baseURL + 'products/brands');
  }

  public getProductTypes(): Observable<ProductType[]> {
    return this.httpClient.get<ProductType[]>(this.baseURL + 'products/types');
  }
}
