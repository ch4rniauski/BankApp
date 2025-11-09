import {Component} from '@angular/core';
import {Sidebar} from '../../common-ui/sidebar/sidebar';
import {CardsArea} from '../../common-ui/cards-area/cards-area';

@Component({
  selector: 'app-main-page',
  imports: [
    Sidebar,
    CardsArea
  ],
  templateUrl: './main-page.html',
  styleUrl: './main-page.scss',
})
export class MainPage {
}
