import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, tap, throwError } from "rxjs";
import { ToastrService } from 'ngx-toastr';
import { Router } from "@angular/router";

@Injectable()
export class AuthTokenInterceptor implements HttpInterceptor {
  token!: string | null;

  constructor(private toastr: ToastrService,private router: Router){}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.token = window.localStorage.getItem('Token');

    req = req.clone({
        setHeaders: { Authorization: `Bearer ${this.token}`}
    });

    return next.handle(req).pipe(tap({
      error: err => { 
        if (err instanceof HttpErrorResponse) {
          if (err.status == 401 || err.status == 403) 
          {
            this.toastr.warning('<small>Sua sessão expirou!</small>');
            window.localStorage.clear();
            this.router.navigateByUrl('/');
          }
          else
            throwError(() => new Error(err.message))          
        }
      },
    }));
  };
};

