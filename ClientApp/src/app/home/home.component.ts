/*import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
}*/
import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { Manager } from '../_models';
import { ManagerService } from '../_services/Manager.service';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit {
  currentManager: Manager;
  managers: Manager[] = [];

  constructor(private managerService: ManagerService) {
    this.currentManager = JSON.parse(localStorage.getItem('currentManager'));
  }

  ngOnInit() {
    this.loadAllManagers();
  }

  deleteUser(id: number) {
    this.managerService.delete(id).pipe(first()).subscribe(() => {
      this.loadAllManagers()
    });
  }

  private loadAllManagers() {
    this.managerService.getAll().pipe(first()).subscribe(managers => {
      this.managers = managers;
    });
  }
}
