import {Component, inject} from '@angular/core';
import {CreditCardService} from '../../data/services/credit-card.service';
import {RouterLink} from '@angular/router';
import {CreditCard} from '../credit-card/credit-card';

@Component({
  selector: 'app-cards-area',
  imports: [
    RouterLink,
    CreditCard
  ],
  templateUrl: './cards-area.html',
  styleUrl: './cards-area.scss',
})
export class CardsArea {
  private creditCardService = inject(CreditCardService)

  clientCreditCards : GetCreditCardResponse[] = []
  lastUpdateTime!: string

  ngOnInit() {
    this.creditCardService.getCreditCardsByClientId()
      .subscribe(res => {
        this.getLastUpdateTime()
        console.log(res) // DELETE AFTER
        this.clientCreditCards = res
      })
  }

  getLastUpdateTime() {
    const now = new Date();

    const hours = now.getHours().toString().padStart(2, '0');
    const minutes = now.getMinutes().toString().padStart(2, '0');

    this.lastUpdateTime = `${hours}:${minutes}`;
  }

  refreshBalanceClick() {

  }
}
