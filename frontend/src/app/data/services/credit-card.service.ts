import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {catchError, Observable, of, throwError} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CreditCardService {
  private httpClient = inject(HttpClient)
  private baseUrl = 'http://localhost:8081/api/creditcards/'

  getCreditCardsByClientId() : Observable<GetCreditCardResponse[]>{
    const clientId = localStorage.getItem('client_id')

    if (!clientId) {
      return of([]);
    }

    return this.httpClient.get<GetCreditCardResponse[]>(`${this.baseUrl}clients/${clientId}`)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          console.error(error)

          return of([]);
        })
      );
  }

  createCreditCard(cardType: string) : Observable<CreateCreditCardResponse | null> {
    const clientId = localStorage.getItem('client_id')

    if (!clientId) {
      return of(null);
    }
    let cardTypeNumber: number

    switch (cardType) {
      case 'Visa':
        cardTypeNumber = 0
        break;
      case 'Mastercard':
        cardTypeNumber = 1
        break;
      case 'Mir':
        cardTypeNumber = 2
        break;
      default:
        cardTypeNumber = 0
        break;
    }

    const request: CreateCreditCardRequest = {
      cardHolderId: clientId,
      cardType: cardTypeNumber
    }

    return this.httpClient.post<CreateCreditCardResponse>(this.baseUrl, request)
      .pipe(
        catchError(
          (error: HttpErrorResponse) => throwError(() => error)
        )
      )
  }
}
