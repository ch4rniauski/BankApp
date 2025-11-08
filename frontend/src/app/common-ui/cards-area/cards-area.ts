import {Component, inject} from '@angular/core';
import {CreditCardService} from '../../data/services/credit-card.service';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-cards-area',
  imports: [
    RouterLink
  ],
  templateUrl: './cards-area.html',
  styleUrl: './cards-area.scss',
})
export class CardsArea {
  private creditCardService = inject(CreditCardService)

  clientCreditCards : GetCreditCardResponse[] = []

  ngOnInit() {
    this.creditCardService.getCreditCardsByClientId()
      .subscribe(res => {
        console.log(res);
        this.clientCreditCards = res;
      })
  }
}
