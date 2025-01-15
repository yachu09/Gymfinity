import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  standalone: false,
  
  templateUrl: './pager.component.html',
  styleUrl: './pager.component.css'
})
export class PagerComponent implements OnInit{
  @Input() totalCount!: number;
  @Input() pageSize!: number;
  @Output() pageChanged = new EventEmitter<number>();

  constructor() { }

  ngOnInit() {

  }

  onPagerChange(event: any) {
    this.pageChanged.emit(event.page)
  }
}
