import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';
import { UserLogin } from '../../models/userLogin.model';
import { HomeImageComponent } from '../home-image/home-image.component';
import { HeaderComponent } from '../header/header.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-form-create',
  imports: [HomeImageComponent, RouterLink, HeaderComponent, FormsModule],
  templateUrl: './form-create.component.html',
  styleUrl: './form-create.component.css',
})
export class FormCreateComponent {
  email: string = '';
  password: string = '';

  get userLogin(): UserLogin {
    return {
      email: this.email,
      password: this.password,
    };
  }

  users: User[] = [];
  private user: User = {
    name: 'testeangular',
    email: 'testeangular',
    password: 'testeangular',
  };

  constructor(private userService: UserService) {}

  /* getUsers() {
    this.userService.getUsers().subscribe((users) => {
      this.users = users;
    });
  }
  /*  

  /* createUser() {
    this.userService.createUser(this.user).subscribe(
      (response) => console.log(response),
      (error: any) => console.log(error),
      () => console.log('Criado com sucesso')
    );
  }
   */

  login() {
    this.userService.login(this.userLogin).subscribe(
      (response) => console.log(response),
      (error: any) => console.log(error),
      () => console.log('Login Feito')
    );
    console.log(this.email);
  }
}
