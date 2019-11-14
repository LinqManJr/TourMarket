import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add authorization header with jwt token if available
        let currentManager = JSON.parse(localStorage.getItem('currentManager'));
        if (currentManager && currentManager.token) {
            request = request.clone({
                setHeaders: { 
                    Authorization: `Bearer ${currentManager.token}`
                }
            });
        }

        return next.handle(request);
    }
}
