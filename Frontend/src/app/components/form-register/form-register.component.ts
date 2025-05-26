import { Component } from '@angular/core';
import { HomeImageComponent } from '../home-image/home-image.component';
import { RouterLink } from '@angular/router';
import { HeaderComponent } from '../header/header.component';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-form-register',
  imports: [HomeImageComponent, RouterLink, HeaderComponent, FormsModule],
  templateUrl: './form-register.component.html',
  styleUrl: './form-register.component.css',
})
export class FormRegisterComponent {
  name: string = '';
  email: string = '';
  password: string = '';
  cfPassword: string = '';

  get userRegister(): User {
    return {
      name: this.name,
      email: this.email,
      password: this.password,
    };
  }

  constructor(private userService: UserService) {}

  crateUser() {
    this.userService.createUser(this.userRegister).subscribe(
      (response) => console.log(response),
      (error: any) => console.log(error),
      () => console.log('Cadastro Feito com Sucesso')
    );
  }
}
