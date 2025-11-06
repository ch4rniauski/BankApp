import {Component, inject} from '@angular/core';
import {Sidebar} from '../../common-ui/sidebar/sidebar';
import {AuthService} from '../../data/services/auth.service';
import {AsyncPipe} from '@angular/common';

@Component({
  selector: 'app-main-page',
  imports: [
    Sidebar,
    AsyncPipe
  ],
  templateUrl: './main-page.html',
  styleUrl: './main-page.scss',
})
export class MainPage {
  private authService = inject(AuthService);

  isAuth$ = this.authService.isAuth();
}
