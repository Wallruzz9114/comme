import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { PaginationModule } from 'ngx-bootstrap/pagination';

import { PagerComponent } from './components/pager/pager.component';
import {
  PaginationHeaderComponent,
} from './components/pagination-header/pagination-header.component';

@NgModule({
  declarations: [PaginationHeaderComponent, PagerComponent],
  imports: [CommonModule, PaginationModule.forRoot(), CarouselModule.forRoot()],
  exports: [PaginationModule, PaginationHeaderComponent, PagerComponent, CarouselModule],
})
export class SharedModule {}
