import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Tour } from '../_models';

@Injectable()
export class TourService {

  constructor(private http: HttpClient) { }

  getTours() {
    return this.http.get<Tour[]>(`${environment.apiUrl}/api/tour/GetTours`);
  }
}
