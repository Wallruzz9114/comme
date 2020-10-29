import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Order } from './../models/order';
import { OrdersService } from './orders.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss'],
})
export class OrdersComponent implements OnInit {
  public orders: Order[];

  constructor(private ordersService: OrdersService) {}

  ngOnInit(): void {
    this.getOrders();
  }

  public getOrders(): void {
    this.ordersService.getOrdersForUser().subscribe((orders: Order[]) => (this.orders = orders)),
      (error: HttpErrorResponse) => console.log(error);
  }
}
