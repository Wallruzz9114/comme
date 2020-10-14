import { Address } from './address';

export interface OrderRequest {
  cartId: string;
  deliveryMethodId: number;
  shippingAddress: Address;
}
