import {Component, inject, Input} from '@angular/core';
import {CreditCardService} from '../../data/services/credit-card.service';

@Component({
  selector: 'app-credit-card',
  imports: [],
  templateUrl: './credit-card.html',
  styleUrl: './credit-card.scss',
})
export class CreditCard {
  private creditCardService = inject(CreditCardService)

  @Input() lastUpdateTime!: string;
}
