import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import {catchError, map, Observable, of, tap, throwError} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private httpClient = inject(HttpClient);
  private baseUrl = 'http://localhost:8080/api/clients/';

  isAuth() : Observable<boolean> {
    const token = localStorage.getItem('access_token');

    if (!token) {
      return of(false);
    }

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    })

    return this.httpClient.get(`${this.baseUrl}is-auth`, { headers })
      .pipe(
        map(() => true),
        catchError(() => of(false))
    )
  }

  registerClient(client: RegisterClientRequest) : Observable<RegisterClientResponse> {
    return this.httpClient.post<RegisterClientResponse>(`${this.baseUrl}register`, client)
      .pipe(
        catchError(
          (error: HttpErrorResponse) => throwError(() => error)
        )
      )
  }

  loginClient(request : LoginClientRequest) : Observable<LoginClientResponse> {
    return this.httpClient.post<LoginClientResponse>(`${this.baseUrl}login`, request)
      .pipe(
        catchError(
          (error: HttpErrorResponse) => throwError(() => error)
        )
      )
  }

  updateAccessToken() : Observable<boolean> {
    const refreshToken = localStorage.getItem('refresh_token');

    if (!refreshToken) {
      return of(false);
    }

    const clientId = localStorage.getItem('client_id');

    if (!clientId) {
      return of(false);
    }

    const request: UpdateAccessTokenRequest = {
      refreshToken: refreshToken,
      clientId: clientId
    }

    return this.httpClient.post<UpdateAccessTokenResponse>(`${this.baseUrl}access-token`, request)
      .pipe(
        tap(res => {
          localStorage.setItem('access_token', res.accessToken);
        }),
        map(() => true),
        catchError(
          (error: HttpErrorResponse) => throwError(() => error)
        )
      )
  }
}
