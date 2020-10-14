import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AccountService } from './../account/account.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit {
  public checkoutForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private accountService: AccountService) {}

  ngOnInit(): void {
    this.initilalizeCheckoutForm();
    this.fullAddressForm();
  }

  public initilalizeCheckoutForm(): void {
    this.checkoutForm = this.formBuilder.group({
      addressForm: this.formBuilder.group({
        street: [null, Validators.required],
        city: [null, Validators.required],
        province: [null, Validators.required],
        postalCode: [null, Validators.required],
        country: [null, Validators.required],
      }),
      deliveryForm: this.formBuilder.group({ deliveryMethod: [null, Validators.required] }),
      paymentForm: this.formBuilder.group({ nameOnCard: [null, Validators.required] }),
    });
  }

  public fullAddressForm() {
    this.accountService.getUserAddress().subscribe(
      (address) => {
        if (address) {
          this.checkoutForm.get('addressForm').patchValue(address);
        }
      },
      (error) => console.log(error)
    );
  }
}
