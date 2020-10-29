import { Component, OnInit } from '@angular/core';
import { AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { AccountService } from './../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  public registerForm: FormGroup;
  public errors: string[];

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeRegisterForm();
  }

  private initializeRegisterForm(): void {
    this.registerForm = this.formBuilder.group({
      fullName: [null, [Validators.required]],
      email: [
        null,
        [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
        [this.validateEmailUniqueness()],
      ],
      password: [null, [Validators.required]],
    });
  }

  public submit(): void {
    this.accountService.register(this.registerForm.value).subscribe(
      () => this.router.navigateByUrl('/shop'),
      (error) => {
        console.log(error);
        this.errors = error.errors;
      }
    );
  }

  public validateEmailUniqueness(): AsyncValidatorFn {
    return (control) => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.accountService.emailExists(control.value).pipe(
            map((exists) => {
              return exists ? { emailExists: true } : null;
            })
          );
        })
      );
    };
  }
}
