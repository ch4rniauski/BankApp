import {Component} from '@angular/core';
import {Sidebar} from '../../common-ui/sidebar/sidebar';
import {CardsArea} from '../../common-ui/cards-area/cards-area';
import {MoneyTransferToCard} from '../../common-ui/money-transfer-to-card/money-transfer-to-card';

@Component({
  selector: 'app-main-page',
  imports: [
    Sidebar,
    CardsArea,
    MoneyTransferToCard
  ],
  templateUrl: './main-page.html',
  styleUrl: './main-page.scss',
})
export class MainPage {
}
