import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from '../../../Services/Common/order.service';
import { Order } from '../../../Models/Order.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrl: './order-detail.component.scss'
})
export class OrderDetailComponent {
  orderId: number;
  order: Order; 

  constructor(private route: ActivatedRoute, 
    private orderService: OrderService,
    private toastr:ToastrService
    ) { }

  ngOnInit(): void {
    this.orderId = this.route.snapshot.params['id']; 
    this.loadOrderDetails();
    
  }

  loadOrderDetails() {
    this.orderService.getOrderById(this.orderId).subscribe((res) => {
      this.order = res?.data; 
    console.log(this.order);

    },(err)=>{
      this.toastr.error(err?.error?.error)
    });
  }
}
