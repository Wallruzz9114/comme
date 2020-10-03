import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss'],
})
export class PagerComponent implements OnInit {
  @Input() totalItemsCount: number;
  @Input() pageSize: number;
  @Output() eventEmitter = new EventEmitter<number>();

  constructor() {}

  ngOnInit(): void {}

  public changePage(pageChangedEvent: PageChangedEvent) {
    this.eventEmitter.emit(pageChangedEvent.page);
  }
}
