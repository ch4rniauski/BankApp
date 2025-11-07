import { Component } from '@angular/core';
import {Sidebar} from '../../common-ui/sidebar/sidebar';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-login-page',
  imports: [
    Sidebar,
    RouterLink
  ],
  templateUrl: './login-page.html',
  styleUrl: './login-page.scss',
})
export class LoginPage {

}
