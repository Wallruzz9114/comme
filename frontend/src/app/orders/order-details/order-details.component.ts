import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

import { Order } from './../../models/order';
import { OrdersService } from './../orders.service';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss'],
})
export class OrderDetailsComponent implements OnInit {
  public order: Order;

  constructor(
    private activatedRoute: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private ordersService: OrdersService
  ) {}

  ngOnInit(): void {
    this.breadcrumbService.set('@OrderDetails', ' ');
    this.getOrder();
  }

  public getOrder(): void {
    this.ordersService.getOrder(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(
      (order: Order) => {
        this.order = order;
        this.breadcrumbService.set('@OrderDetails', `Order# ${order.id} - ${order.orderStatus}`);
      },
      (error) => console.log(error)
    );
  }
}
