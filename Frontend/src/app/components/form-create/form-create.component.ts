import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';
import { HomeImageComponent } from '../home-image/home-image.component';

@Component({
  selector: 'app-form-create',
  imports: [HomeImageComponent, RouterLink],
  templateUrl: './form-create.component.html',
  styleUrl: './form-create.component.css',
})
export class FormCreateComponent {
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
}
