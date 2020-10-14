import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-checkout-steps',
  templateUrl: './checkout-steps.component.html',
  styleUrls: ['./checkout-steps.component.scss'],
  providers: [{ provide: CdkStepper, useExisting: CheckoutStepsComponent }],
})
export class CheckoutStepsComponent extends CdkStepper implements OnInit {
  @Input() linearModeSelected: boolean;

  ngOnInit(): void {
    this.linear = this.linearModeSelected;
  }

  public goToStep(index: number) {
    this.selectedIndex = index;
    console.log(this.selectedIndex);
  }
}
