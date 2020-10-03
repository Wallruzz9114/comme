import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PaginationModule } from 'ngx-bootstrap/pagination';

import { PagerComponent } from './components/pager/pager.component';
import {
  PaginationHeaderComponent,
} from './components/pagination-header/pagination-header.component';

@NgModule({
  declarations: [PaginationHeaderComponent, PagerComponent],
  imports: [CommonModule, PaginationModule.forRoot()],
  exports: [PaginationModule, PaginationHeaderComponent, PagerComponent],
})
export class SharedModule {}
