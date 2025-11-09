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

  creditCardLogoPath!: string

  ngOnChanges(): void {
    this.changeCreditCardLogo()
  }

  changeCreditCardLogo() {
    switch(this.creditCard?.cardType){
      case 'Visa':
        this.creditCardLogoPath = '/assets/imgs/visa-logo.svg'
        break
      case 'Mir':
        this.creditCardLogoPath = '/assets/imgs/mir-logo.png'
        break
      case 'Mastercard':
        this.creditCardLogoPath = '/assets/imgs/mastercard-logo.svg'
        break
      default:
        this.creditCardLogoPath = '/assets/imgs/visa-logo.svg'
        break
    }
  }
}
