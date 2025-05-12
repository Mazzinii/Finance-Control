import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HomeImageComponent } from './components/home-image/home-image.component';
import { FormCreateComponent } from './components/form-create/form-create.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HomeImageComponent, FormCreateComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'Frontend';
  componentPai = 'App';
}
