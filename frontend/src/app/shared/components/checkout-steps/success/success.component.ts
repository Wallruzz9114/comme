import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Order } from './../../../../models/order';

@Component({
  selector: 'app-success',
  templateUrl: './success.component.html',
  styleUrls: ['./success.component.scss'],
})
export class SuccessComponent implements OnInit {
  public order: Order;

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    const state = navigation && navigation.extras && navigation.extras.state;
    if (state) {
      this.order = state as Order;
    }
  }

  ngOnInit(): void {}
}
