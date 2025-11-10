import { Routes } from '@angular/router';
import {MainPage} from './pages/main-page/main-page';
import {LoginPage} from './pages/login-page/login-page';
import {RegisterPage} from './pages/register-page/register-page';
import {AddCreditCardPage} from './pages/add-credit-card-page/add-credit-card-page';
import {MoneyTransferToCardPage} from './pages/money-transfer-to-card-page/money-transfer-to-card-page';

export const routes: Routes = [
  {path: '', component: MainPage},
  {path: 'login', component: LoginPage},
  {path: 'register', component: RegisterPage},
  {path: 'add-credit-card', component: AddCreditCardPage},
  {path: 'transfer-money-to-card', component:MoneyTransferToCardPage}
];
