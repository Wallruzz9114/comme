import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { environment } from './../../../environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss'],
})
export class TestErrorComponent implements OnInit {
  public baseURL = environment.apiURL;
  public validationErrors: any;

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {}

  public getNotFoundError() {
    this.httpClient.get(this.baseURL + 'products/50').subscribe(
      (response) => console.log(response),
      (error) => console.log(error)
    );
  }

  public getServerError() {
    this.httpClient.get(this.baseURL + 'bug/servererror').subscribe(
      (response) => console.log(response),
      (error) => console.log(error)
    );
  }

  public getBadRequestError() {
    this.httpClient.get(this.baseURL + 'bug/badrequest').subscribe(
      (response) => console.log(response),
      (error) => console.log(error)
    );
  }

  public getValidationError() {
    this.httpClient.get(this.baseURL + 'products/fortytwo').subscribe(
      (response) => console.log(response),
      (error) => {
        console.log(error);
        this.validationErrors = error.errors;
      }
    );
  }
}
