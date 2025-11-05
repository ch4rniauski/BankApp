import {Component} from '@angular/core';
import {Sidebar} from '../../common-ui/sidebar/sidebar';

@Component({
  selector: 'app-main-page',
  imports: [
    Sidebar
  ],
  templateUrl: './main-page.html',
  styleUrl: './main-page.scss',
})
export class MainPage {

}
