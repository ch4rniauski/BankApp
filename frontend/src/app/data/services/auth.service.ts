import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {catchError, map, Observable, of} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  httpClient = inject(HttpClient);
  url = 'http://localhost:8080/api/clients/is-auth';

  isAuth() : Observable<boolean> {
    const token = localStorage.getItem('access_token');

    if (!token) {
      return of(false);
    }

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    })

    return this.httpClient.post(this.url, null, {headers})
      .pipe(
        map(() => true),
        catchError(() => of(false))
    )
  }

  registerClient(){

  }
}
