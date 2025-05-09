import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable, tap } from 'rxjs';
import { BaseService } from '../../base.service';

@Injectable({ providedIn: 'root' })
export class AuthService extends BaseService {
  private apiUrl = environment.apiUrl;

  constructor(http: HttpClient) {
    super(http);
  }

  login(username: string, password: string): Observable<any> {
    return this.http
      .post(`${this.apiUrl}/auth/login`, {
        email: username,
        password: password,
      })
      .pipe(
        tap((res: any) => {
          localStorage.setItem('userId', res.userId);
        })
      );
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('userId');
  }

  getUserId(): string {
    return localStorage.getItem('userId') || '';
  }

  logout() {
    localStorage.removeItem('userId');
  }
}
