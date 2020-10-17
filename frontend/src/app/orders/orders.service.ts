import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from './../../environments/environment';
import { Order } from './../models/order';

@Injectable({
  providedIn: 'root',
})
export class OrdersService {
  public baseURL = environment.backendURL;

  constructor(private httpClient: HttpClient) {}

  public getOrdersForUser(): Observable<Order[]> {
    return this.httpClient.get<Order[]>(this.baseURL + 'orders');
  }

  public getOrder(orderId: number): Observable<Order> {
    return this.httpClient.get<Order>(this.baseURL + `orders/${orderId}`);
  }
}
