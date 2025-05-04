import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from './auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const modifiedReq = req.clone({
    setHeaders: {
      'X-User-ID': authService.getUserId(),
    },
  });
  return next(modifiedReq);
};