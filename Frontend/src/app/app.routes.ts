import { Routes } from '@angular/router';
import { FormCreateComponent } from './components/create/create.component';
import { FormRegisterComponent } from './components/register/register.component';
import { UserDashboardComponent } from './components/user-dashboard/user-dashboard.component';
import { SummaryComponent } from './components/summary/summary.component';

export const routes: Routes = [
  {
    path: 'home',
    component: FormCreateComponent,
  },
  {
    path: 'register',
    component: FormRegisterComponent,
  },
  {
    path: 'dashboard',
    component: UserDashboardComponent,
  },
  {
    path: 'summary',
    component: SummaryComponent,
  },
];
