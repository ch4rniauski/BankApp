import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-credit-card',
  imports: [],
  templateUrl: './credit-card.html',
  styleUrl: './credit-card.scss',
})
export class CreditCard {
  @Input() lastUpdateTime!: string;
}
