import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import {catchError, map, Observable, of, throwError} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  httpClient = inject(HttpClient);
  baseUrl = 'http://localhost:8080/api/clients/';

  isAuth() : Observable<boolean> {
    const token = localStorage.getItem('access_token');

    if (!token) {
      return of(false);
    }

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    })

    return this.httpClient.get(`${this.baseUrl}is-auth`, {headers})
      .pipe(
        map(() => true),
        catchError(() => of(false))
    )
  }

  registerClient(client: RegisterClientRequest) : Observable<RegisterClientResponse> {
    return this.httpClient.post<RegisterClientResponse>(`${this.baseUrl}register`, client)
      .pipe(
        catchError((error: HttpErrorResponse) => throwError(() => error))
      )
  }

  loginClient(request : LoginClientRequest) : Observable<LoginClientResponse> {
    return this.httpClient.post<LoginClientResponse>(`${this.baseUrl}login`, request)
      .pipe(
        catchError((error: HttpErrorResponse) => throwError(() => error))
      )
  }
}
