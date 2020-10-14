import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { CartService } from './../../../../cart/cart.service';
import { CheckoutService } from './../../../../checkout/checkout.service';
import { DeliveryMethod } from './../../../../models/delivery-method';

@Component({
  selector: 'app-select-delivery',
  templateUrl: './select-delivery.component.html',
  styleUrls: ['./select-delivery.component.scss'],
})
export class SelectDeliveryComponent implements OnInit {
  @Input() checkoutForm: FormGroup;
  public allDeliveryMethods: DeliveryMethod[];

  constructor(private checkoutService: CheckoutService, private cartService: CartService) {}

  ngOnInit(): void {
    this.getDeliveryMethods();
  }

  public getDeliveryMethods(): void {
    this.checkoutService.getDeliveryMethods().subscribe(
      (deliveryMothods: DeliveryMethod[]) => (this.allDeliveryMethods = deliveryMothods),
      (error: any) => console.log(error)
    );
  }

  public setShippingPrice(deliveryMethod: DeliveryMethod): void {
    this.cartService.setShippingPrice(deliveryMethod);
  }
}
