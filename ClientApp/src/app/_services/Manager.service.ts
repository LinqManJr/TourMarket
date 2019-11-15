import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Manager } from '../_models';

@Injectable()
export class ManagerService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Manager[]>(`${environment.apiUrl}/managers`);
    }

    getById(id: number) {
        return this.http.get(`${environment.apiUrl}/managers/` + id);
    }

    register(manager: Manager) {
        return this.http.post(`${environment.apiUrl}/managers/register`, manager);
    }

    update(manager: Manager) {
        return this.http.put(`${environment.apiUrl}/managers/` + manager.id, manager);
    }

    delete(id: number) {
        return this.http.delete(`${environment.apiUrl}/managers/` + id);
    }
}
