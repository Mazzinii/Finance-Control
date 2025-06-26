import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';
import { UserLogin } from '../../models/userLogin.model';
import { HomeImageComponent } from '../home-image/home-image.component';
import { HeaderComponent } from '../header/header.component';
import { FormsModule } from '@angular/forms';
import { LoginResponse } from '../../models/loginResponse.model';

@Component({
  selector: 'app-form-create',
  imports: [HomeImageComponent, RouterLink, HeaderComponent, FormsModule],
  templateUrl: './create.component.html',
  styleUrl: './create.component.css',
})
export class FormCreateComponent {
  email: string = '';
  password: string = '';

  erroLogin: boolean = false;
  erroMessage: string = '';

  data: LoginResponse = {
    token: '',
    userId: '',
  };

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

  constructor(private userService: UserService, private router: Router) {}

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
    var response = this.userService.login(this.userLogin).subscribe(
      (response) =>
        (this.data = {
          token: (response as any).token,
          userId: (response as any).userId,
        }),
      (error: any) =>
        console.log(error, (this.erroMessage = 'Email ou Senha Incorretos')),
      () => {
        this.router.navigate(['/dashboard']);
        console.log(this.data.token);
      }
    );
  }
}
