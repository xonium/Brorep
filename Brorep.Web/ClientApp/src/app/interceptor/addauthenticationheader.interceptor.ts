import {
    HttpEvent,
    HttpInterceptor,
    HttpHandler,
    HttpRequest,
  } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JwtService } from '../services/jwt.service';
import { Injectable } from '@angular/core';

@Injectable()
  export class AddAuthenticationInterceptor implements HttpInterceptor {
    constructor(public jwtService: JwtService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      // Clone the request to add the new header
      const token = this.jwtService.getToken();
      if (token) {
        const clonedRequest = req.clone({ headers: req.headers.set('Authorization', 'Bearer ' + token) });
        return next.handle(clonedRequest);
      } else {
        return next.handle(req);
      }
    }
  }
