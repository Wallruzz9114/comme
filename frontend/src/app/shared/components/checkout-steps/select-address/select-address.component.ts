import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

import { AccountService } from './../../../../account/account.service';

@Component({
  selector: 'app-select-address',
  templateUrl: './select-address.component.html',
  styleUrls: ['./select-address.component.scss'],
})
export class SelectAddressComponent implements OnInit {
  @Input() checkoutForm: FormGroup;

  constructor(private accountService: AccountService, private toastrService: ToastrService) {}

  ngOnInit(): void {}

  public updateUserAddress(): void {
    this.accountService.updateUserAddress(this.checkoutForm.get('addressForm').value).subscribe(
      () => this.toastrService.success('Address successfully updated'),
      (error) => {
        this.toastrService.error(error);
        console.log(error);
      }
    );
  }
}
