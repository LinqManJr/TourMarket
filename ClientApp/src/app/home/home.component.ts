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
import { UserService } from '../_services';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit {
  currentManager: Manager;
  managers: Manager[] = [];

  constructor(private userService: UserService) {
    this.currentManager = JSON.parse(localStorage.getItem('currentManager'));
  }

  ngOnInit() {
    this.loadAllManagers();
  }

  deleteUser(id: number) {
    this.ManagerService.delete(id).pipe(first()).subscribe(() => {
      this.loadAllManagers()
    });
  }

  private loadAllUsers() {
    this.userService.getAll().pipe(first()).subscribe(managers => {
      this.managers = managers;
    });
  }
}
