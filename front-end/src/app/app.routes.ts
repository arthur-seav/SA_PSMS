import { Routes } from '@angular/router';
import {Login} from './features/login/login';
import {Home} from './features/home/home';
import {authGuard} from './services/auth-guard';

export const routes: Routes = [
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {path: 'login', component: Login},
  {path: 'home', component: Home, canActivate: [authGuard] },
];
