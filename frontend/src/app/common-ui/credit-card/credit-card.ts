import {Component, Input} from '@angular/core';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-credit-card',
  imports: [
    RouterLink
  ],
  templateUrl: './credit-card.html',
  styleUrl: './credit-card.scss',
})
export class CreditCard {
  @Input() lastUpdateTime!: string
  @Input() creditCard!: GetCreditCardResponse;
}
