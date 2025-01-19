import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IBasket, IBasketItem } from '../models/basket';
import { BasketService } from '../../basket/basket.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-basket-summary',
  standalone: false,

  templateUrl: './basket-summary.component.html',
  styleUrl: './basket-summary.component.css'
})
export class BasketSummaryComponent implements OnInit {
  basket$!: Observable<IBasket | null >;

  @Output() decrement: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() increment: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() remove: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Input() isBasket = true;

  constructor(private basketService: BasketService) { }

  ngOnInit() {
    this.basket$ = this.basketService.basket$;
  }

  decrementItemQuantity(item: IBasketItem) {
    this.decrement.emit(item);
  }

  incrementItemQuantity(item: IBasketItem) {
    this.increment.emit(item);
  }

  removeBasketItem(item: IBasketItem) {
    this.remove.emit(item);
  }
}
