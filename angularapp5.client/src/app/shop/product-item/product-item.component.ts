import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from '../../shared/models/product';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-product-item',
  standalone: false,
  
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.css'
})
export class ProductItemComponent implements OnInit {
  @Input() product!: IProduct;

  constructor(private basketService: BasketService) { }

  ngOnInit() {

  }

  addItemToBasket() {
    this.basketService.addItemToBasket(this.product);
  }
}
