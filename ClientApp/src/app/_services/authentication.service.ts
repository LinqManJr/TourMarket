import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';

@Injectable()
export class AuthenticationService {
    constructor(private http: HttpClient) { }

    login(login: string, password: string) {
        return this.http.post<any>(`${environment.apiUrl}/api/account/login`, { login: login, password: password })
            .pipe(map(manager => {
                // login successful if there's a jwt token in the response
                if (manager && manager.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentManager', JSON.stringify(manager));
                }

                return manager;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentManager');
    }
}
