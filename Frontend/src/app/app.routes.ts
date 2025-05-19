import { Routes } from '@angular/router';
import { FormCreateComponent } from './components/form-create/form-create.component';
import { FormRegisterComponent } from './components/form-register/form-register.component';

export const routes: Routes = [
  {
    path: 'home',
    component: FormCreateComponent,
  },
  {
    path: 'register',
    component: FormRegisterComponent,
  },
];
