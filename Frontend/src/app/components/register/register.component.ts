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
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class FormRegisterComponent {
  name: string = '';
  email: string = '';
  password: string = '';
  cfPassword: string = '';
  message: string = '';
  erroMessage: string = '';
  class: string = '';

  get userRegister(): User {
    return {
      name: this.name,
      email: this.email,
      password: this.password,
    };
  }

  postStatus = true;

  constructor(private userService: UserService) {}

  crateUser() {
    this.checkPassword();
    if (this.postStatus == false) {
      this.postStatus = true;
    } else {
      this.userService.createUser(this.userRegister).subscribe(
        (response) => console.log(response),
        (error: any) =>
          console.log(
            error,
            ((this.erroMessage = 'Este e-mail já está vinculado a uma conta'),
            (this.class = 'error'))
          ),
        () => ((this.message = 'Cadastro Realizado'), (this.class = 'message'))
      );
    }
  }

  checkPassword() {
    if (this.password != this.cfPassword) {
      this.erroMessage = 'As senhas devem ser iguais!';
      this.class = 'error';
      this.postStatus = false;
    }
  }
}
