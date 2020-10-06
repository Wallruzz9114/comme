import { v4 as uuidv4 } from 'uuid';

import { CartItem } from './cart-item';

export interface Cart {
  id: string;
  cartItems: CartItem[];
}

export class CustomerCart implements Cart {
  id: string = uuidv4();
  cartItems: CartItem[] = [];
}
