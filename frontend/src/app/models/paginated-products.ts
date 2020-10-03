import { Product } from './product';

export interface PaginatedProducts {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: Product[];
}
