import { Component } from '@angular/core';
import { HomeImageComponent } from '../home-image/home-image.component';
import { RouterLink } from '@angular/router';
import { HeaderComponent } from '../header/header.component';

@Component({
  selector: 'app-form-register',
  imports: [HomeImageComponent, RouterLink, HeaderComponent],
  templateUrl: './form-register.component.html',
  styleUrl: './form-register.component.css',
})
export class FormRegisterComponent {}
