import {inject, Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class MoneyTransferService {
  private httpClient = inject(HttpClient)
  private baseUrl = 'http://localhost:8082/api/moneytransfer/'

  transferMoney(): Observable<TransferMoneyResponse> {
    return this.httpClient.post<TransferMoneyResponse>(`${this.baseUrl}transfer`, )
  }
}
