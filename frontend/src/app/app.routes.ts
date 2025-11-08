import { Routes } from '@angular/router';
import {MainPage} from './pages/main-page/main-page';
import {LoginPage} from './pages/login-page/login-page';
import {RegisterPage} from './pages/register-page/register-page';

export const routes: Routes = [
  {path: '', component: MainPage},
  {path: 'login', component: LoginPage},
  {path: 'register', component: RegisterPage},
];
