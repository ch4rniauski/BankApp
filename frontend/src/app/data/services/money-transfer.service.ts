import {inject, Injectable} from '@angular/core';
import {catchError, Observable, throwError} from 'rxjs';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class MoneyTransferService {
  private httpClient = inject(HttpClient)
  private baseUrl = 'http://localhost:8082/api/moneytransfer/'

  transferMoney(request: TransferMoneyRequest): Observable<TransferMoneyResponse> {

    return this.httpClient.post<TransferMoneyResponse>(`${this.baseUrl}transfer`, request)
      .pipe(
        catchError((error: HttpErrorResponse) => throwError(() => error))
      )
  }
}
