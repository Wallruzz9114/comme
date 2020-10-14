import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { PaymentComponent } from './../shared/components/checkout-steps/payment/payment.component';
import {
  ReviewOrderComponent,
} from './../shared/components/checkout-steps/review-order/review-order.component';
import {
  SelectAddressComponent,
} from './../shared/components/checkout-steps/select-address/select-address.component';
import {
  SelectDeliveryComponent,
} from './../shared/components/checkout-steps/select-delivery/select-delivery.component';
import { SuccessComponent } from './../shared/components/checkout-steps/success/success.component';
import { SharedModule } from './../shared/shared.module';
import { CheckoutRoutingModule } from './checkout-routing.module';
import { CheckoutComponent } from './checkout.component';

@NgModule({
  declarations: [
    CheckoutComponent,
    SelectAddressComponent,
    SelectDeliveryComponent,
    ReviewOrderComponent,
    PaymentComponent,
    SuccessComponent,
  ],
  imports: [CommonModule, CheckoutRoutingModule, SharedModule],
})
export class CheckoutModule {}
