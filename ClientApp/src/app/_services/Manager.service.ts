import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Manager } from '../_models';

@Injectable()
export class ManagerService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Manager[]>(`${environment.apiUrl}/account`);
    }

    getById(id: number) {
        return this.http.get(`${environment.apiUrl}/account/` + id);
    }

    register(manager: Manager) {
        return this.http.post(`${environment.apiUrl}/api/account/register`, manager);
    }

    update(manager: Manager) {
        return this.http.put(`${environment.apiUrl}/account/` + manager.id, manager);
    }

    delete(id: number) {
        return this.http.delete(`${environment.apiUrl}/account/` + id);
    }
}
