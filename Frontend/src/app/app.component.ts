import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HomeImageComponent } from './components/home-image/home-image.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'Frontend';
  componentPai = 'App';
}
