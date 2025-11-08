import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {catchError, Observable, of, throwError} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CreditCardService {
  private httpClient = inject(HttpClient)
  private baseUrl = 'http://localhost:8081/api/creditcards/';

  getCreditCardsByClientId() : Observable<GetCreditCardResponse[]>{
    const clientId = localStorage.getItem('client_id');

    if (!clientId) {
      return of([]);
    }

    return this.httpClient.get<GetCreditCardResponse[]>(`${this.baseUrl}clientId`)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          console.error(error)

          return of([]);
        })
      );
  }
}
