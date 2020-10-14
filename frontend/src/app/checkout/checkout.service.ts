import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Order } from '../models/order';
import { environment } from './../../environments/environment';
import { DeliveryMethod } from './../models/delivery-method';
import { OrderRequest } from './../models/order-request';

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  private baseURL = environment.backendURL;

  constructor(private httpClient: HttpClient) {}

  public getDeliveryMethods(): Observable<DeliveryMethod[]> {
    return this.httpClient.get<DeliveryMethod[]>(this.baseURL + 'orders/deliverymethods').pipe(
      map((deliveryMethods: DeliveryMethod[]) => {
        return deliveryMethods.sort((a, b) => b.price - a.price);
      })
    );
  }

  public placeOrder(orderRequest: OrderRequest): Observable<Order> {
    return this.httpClient.post<Order>(this.baseURL + 'orders', orderRequest);
  }
}
