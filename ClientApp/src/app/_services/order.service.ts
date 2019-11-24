import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Order } from '../_models';


@Injectable()
export class OrderService {
  constructor(private http: HttpClient) { }

  getOrdersById(id: number) {
    return this.http.get<Order[]>(`${environment.apiUrl}/api/order/GetOrders/` + id);
  }

}
