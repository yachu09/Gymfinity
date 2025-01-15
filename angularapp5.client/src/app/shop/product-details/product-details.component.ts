import { Component, OnInit } from '@angular/core';
import { IProduct } from '../../shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  standalone: false,
  
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent implements OnInit {
  product!: IProduct;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute) {

  }

  ngOnInit() {
    this.loadProduct();
  }

  loadProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id !== null) {
      this.shopService.getProduct(+id)
        .subscribe(product => {
          this.product = product;
        }, error => {
          console.log(error); 
        });
    } else {
      console.error('ID is null');
    }
  }

}
