import { Component, OnInit } from '@angular/core';
import { IOrder } from '../shared/models/order';
import { ActivatedRoute } from '@angular/router';
import { OrdersService } from '../orders/orders.service';

@Component({
  selector: 'app-order-detailed',
  standalone: false,
  
  templateUrl: './order-detailed.component.html',
  styleUrl: './order-detailed.component.css'
})
export class OrderDetailedComponent implements OnInit {
  order?: IOrder;

  constructor(private orderService: OrdersService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    id && this.orderService.getOrderDetailed(+id).subscribe({
      next: order => {
        this.order = order;
      }
    })
  }
}
