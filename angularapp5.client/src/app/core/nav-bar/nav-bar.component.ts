import { Component, OnInit } from '@angular/core';
import { BasketService } from '../../basket/basket.service';
import { IBasket } from '../../shared/models/basket';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-nav-bar',
  standalone: false,

  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent implements OnInit {
  basket$!: Observable<IBasket | null>;

  constructor(private basketService: BasketService) {

  }

  ngOnInit() {
    this.basket$ = this.basketService.basket$;
  }
}
