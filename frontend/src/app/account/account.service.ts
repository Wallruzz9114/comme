import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from './../../environments/environment';
import { Address } from './../models/address';
import { User } from './../models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private baseURL = environment.backendURL;
  private currentUserSource = new ReplaySubject<User>(1);
  public currentUser$ = this.currentUserSource.asObservable();

  constructor(private httpClient: HttpClient, private router: Router) {}

  public login(values: any): Observable<void> {
    return this.httpClient
      .post(this.baseURL + 'account/login', values)
      .pipe(map((user: User) => this.setUser(user)));
  }

  public register(values: any): Observable<void> {
    return this.httpClient
      .post(this.baseURL + 'account/register', values)
      .pipe(map((user: User) => this.setUser(user)));
  }

  public logout(): void {
    localStorage.removeItem(environment.token);
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  public emailExists(email: string): Observable<boolean> {
    const req = this.baseURL + `account/exists?email=${email}`;
    return this.httpClient.get<boolean>(req);
  }

  public setUser(user: User): void {
    if (user) {
      localStorage.setItem(environment.token, user.token);
      this.currentUserSource.next(user);
    }
  }

  public loadCurrentUser(token: string): Observable<void> {
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.httpClient
      .get<User>(this.baseURL + 'account/get', { headers })
      .pipe(
        map((user: User) => {
          if (user) {
            localStorage.setItem(environment.token, user.token);
            this.currentUserSource.next(user);
          }
        })
      );
  }

  public getUserAddress(): Observable<Address> {
    return this.httpClient.get<Address>(this.baseURL + 'account/address');
  }

  public updateUserAddress(address: Address): Observable<Address> {
    return this.httpClient.put<Address>(this.baseURL + 'account/update', address);
  }
}
