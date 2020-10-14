import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Order } from 'src/app/models/order';

import { CartService } from './../../../../cart/cart.service';
import { CheckoutService } from './../../../../checkout/checkout.service';
import { Cart } from './../../../../models/cart';
import { OrderRequest } from './../../../../models/order-request';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss'],
})
export class PaymentComponent implements OnInit {
  @Input() checkoutForm: FormGroup;

  constructor(
    private cartService: CartService,
    private checkoutService: CheckoutService,
    private toastrService: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  public submitOrder(): void {
    const cart: Cart = this.cartService.getCurrentCart();
    const orderRequest: OrderRequest = this.getNewOrderRequest(cart);

    this.checkoutService.placeOrder(orderRequest).subscribe((order: Order) => {
      this.toastrService.success('Order successfully placed');
      this.cartService.emptyCartLocally();

      const navigationExtras: NavigationExtras = { state: order };

      this.router.navigate(['checkout/success'], navigationExtras);
    });
  }

  private getNewOrderRequest(cart: Cart): OrderRequest {
    return {
      cartId: cart.id,
      deliveryMethodId: +this.checkoutForm.get('deliveryForm').get('deliveryMethod').value,
      shippingAddress: this.checkoutForm.get('addressForm').value,
    };
  }
}
