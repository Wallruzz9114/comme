import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from './../../environments/environment';
import { Cart, CustomerCart } from './../models/cart';
import { CartItem } from './../models/cart-item';
import { CartSummary } from './../models/cart-summary';
import { DeliveryMethod } from './../models/delivery-method';
import { Product } from './../models/product';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private cartSource = new BehaviorSubject<Cart>(null);
  private cartSummarySource = new BehaviorSubject<CartSummary>(null);

  private baseURL = environment.backendURL;
  public cart$ = this.cartSource.asObservable();
  public cartSummary$ = this.cartSummarySource.asObservable();
  public shippingPrice: number = 0;

  constructor(private httpClient: HttpClient) {}

  public getCart(id: string): Observable<void> {
    return this.httpClient.get(this.baseURL + `cart?id=${id}`).pipe(
      map((cart: Cart) => {
        this.cartSource.next(cart);
        this.processCartSummary();
      })
    );
  }

  public setOrUpdateCart(cart: Cart): Subscription {
    return this.httpClient.post(this.baseURL + 'cart', cart).subscribe(
      (cart: Cart) => {
        this.cartSource.next(cart);
        this.processCartSummary();
      },
      (error) => console.log(error)
    );
  }

  public getCurrentCart(): Cart {
    return this.cartSource.value;
  }

  public addItemToCart(product: Product, quantity = 1) {
    const cartItem: CartItem = this.getItemFromProduct(product, quantity);
    const cart: Cart = this.getCurrentCart() ?? this.addNewCart();

    cart.cartItems = this.addOrUpdateCartItems(cart.cartItems, cartItem, quantity);
    this.setOrUpdateCart(cart);
  }

  public incrementCartItemQuantity(cartItem: CartItem): void {
    const currentCart: Cart = this.getCurrentCart();
    const foundCartItemIndex = currentCart.cartItems.findIndex((item) => item.id === cartItem.id);
    currentCart.cartItems[foundCartItemIndex].quantity++;

    this.setOrUpdateCart(currentCart);
  }

  public decrementCartItemQuantity(CartItem: CartItem): void {
    const currentCart: Cart = this.getCurrentCart();
    const foundCartItemIndex = currentCart.cartItems.findIndex((item) => item.id === CartItem.id);

    if (currentCart.cartItems[foundCartItemIndex].quantity > 1) {
      currentCart.cartItems[foundCartItemIndex].quantity--;
      this.setOrUpdateCart(currentCart);
    } else {
      this.removeItemFromCart(currentCart.cartItems[foundCartItemIndex]);
    }
  }

  public removeItemFromCart(CartItem: CartItem): void {
    const currentCart: Cart = this.getCurrentCart();

    if (currentCart.cartItems.some((item) => item.id === CartItem.id)) {
      currentCart.cartItems = currentCart.cartItems.filter((item) => item.id !== CartItem.id);

      if (currentCart.cartItems.length > 0) {
        this.setOrUpdateCart(currentCart);
      } else {
        this.emptyCart(currentCart);
      }
    }
  }

  public emptyCart(cart: Cart): Subscription {
    return this.httpClient.delete<void>(this.baseURL + `cart?id=${cart.id}`).subscribe(
      () => {
        this.cartSource.next(null);
        this.cartSummarySource.next(null);
        localStorage.removeItem(environment.cartId);
      },
      (error: any) => console.log(error)
    );
  }

  public emptyCartLocally(): void {
    this.cartSource.next(null);
    this.cartSummarySource.next(null);

    localStorage.removeItem(environment.cartId);
  }

  public setShippingPrice(deliveryMethod: DeliveryMethod): void {
    this.shippingPrice = deliveryMethod.price;
    this.processCartSummary();
  }

  private processCartSummary(): void {
    const cart: Cart = this.getCurrentCart();
    const shippingCost = this.shippingPrice;
    const subTotal = cart.cartItems.reduce((total, item) => item.price * item.quantity + total, 0);
    const total = subTotal + shippingCost;

    this.cartSummarySource.next({ shippingCost, total, subTotal });
  }

  private addNewCart(): Cart {
    const cart: CustomerCart = new CustomerCart();
    localStorage.setItem(environment.cartId, cart.id);

    return cart;
  }

  private getItemFromProduct(product: Product, quantity: number): CartItem {
    return {
      id: product.id,
      name: product.name,
      price: product.price,
      pictureURL: product.pictureURL,
      quantity,
      productBrand: product.productBrand,
      productType: product.productType,
    };
  }

  private addOrUpdateCartItems(
    cartItems: CartItem[],
    cartItem: CartItem,
    quantity: number
  ): CartItem[] {
    const itemIndex = cartItems.findIndex((item) => item.id == cartItem.id);

    if (itemIndex === -1) {
      cartItem.quantity = quantity;
      cartItems.push(cartItem);
    } else {
      cartItems[itemIndex].quantity += quantity;
    }

    return cartItems;
  }
}
