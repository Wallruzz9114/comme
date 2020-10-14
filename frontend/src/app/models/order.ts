import { Address } from './address';
import { OrderItem } from './order-item';

export interface Order {
  id: number;
  customerEmail: string;
  orderDate: Date;
  shippingAddress: Address;
  deliveryMethodPrice: number;
  deliveryMethod: string;
  orderItems: OrderItem[];
  subTotal: number;
  orderStatus: string;
  total: number;
  paymentMethodId: string;
}
