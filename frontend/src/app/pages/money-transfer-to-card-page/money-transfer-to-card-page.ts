import { Component } from '@angular/core';
import {Sidebar} from '../../common-ui/sidebar/sidebar';
import {CardsArea} from '../../common-ui/cards-area/cards-area';
import {TransferMoneyToCardForm} from '../../common-ui/transfer-money-to-card-form/transfer-money-to-card-form';

@Component({
  selector: 'app-money-transfer-to-card-page',
  imports: [
    Sidebar,
    CardsArea,
    TransferMoneyToCardForm
  ],
  templateUrl: './money-transfer-to-card-page.html',
  styleUrl: './money-transfer-to-card-page.scss',
})
export class MoneyTransferToCardPage {
  clientCreditCards: GetCreditCardResponse[] = []
  cardNumber = 0
}
