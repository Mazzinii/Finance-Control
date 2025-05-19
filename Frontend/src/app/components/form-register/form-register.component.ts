import { Component } from '@angular/core';
import { HomeImageComponent } from '../home-image/home-image.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-form-register',
  imports: [HomeImageComponent, RouterLink],
  templateUrl: './form-register.component.html',
  styleUrl: './form-register.component.css',
})
export class FormRegisterComponent {}
