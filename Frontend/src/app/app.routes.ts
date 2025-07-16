import { Routes } from '@angular/router';
import { FormCreateComponent } from './components/login/login.component';
import { FormRegisterComponent } from './components/register/register.component';
import { UserDashboardComponent } from './components/user-dashboard/user-dashboard.component';
import { SummaryComponent } from './components/summary/summary.component';
import { DeleteTransationComponent } from './components/modals/delete-transation/delete-transation.component';

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
  {
    path: 'modal',
    component: DeleteTransationComponent,
  },
];
