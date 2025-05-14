import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { FormCreateComponent } from './components/form-create/form-create.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, FormCreateComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'Frontend';
  componentPai = 'App';
}
