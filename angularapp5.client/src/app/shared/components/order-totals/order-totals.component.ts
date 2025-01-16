import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasketTotals } from '../../models/basket';
import { BasketService } from '../../../basket/basket.service';

@Component({
  selector: 'app-order-totals',
  standalone: false,
  
  templateUrl: './order-totals.component.html',
  styleUrl: './order-totals.component.css'
})
export class OrderTotalsComponent implements OnInit {
  basketTotal$!: Observable<IBasketTotals | null>;

  constructor(private basketService: BasketService) { }

  ngOnInit() {
    this.basketTotal$ = this.basketService.basketTotal$;
  }
}
