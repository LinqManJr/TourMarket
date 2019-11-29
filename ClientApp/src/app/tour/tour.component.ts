import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { Tour } from '../_models';
import { TourService } from '../_services';


@Component({
    selector: 'app-tour',
    templateUrl: './tour.component.html',
    styleUrls: ['./tour.component.css']
})
export class TourComponent implements OnInit{

    tours: Tour[] = [];
    
    ngOnInit(): void {
      this.loadTours();
    }    

    constructor(private tourService: TourService) { }

    private loadTours() {
      this.tourService.getTours().pipe(first()).subscribe(tours => { this.tours = tours; });
      console.log(this.tours);
    }
}
