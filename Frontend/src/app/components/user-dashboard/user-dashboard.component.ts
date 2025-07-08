import { Component } from '@angular/core';
import { SummaryComponent } from '../summary/summary.component';
import { TransationService } from '../../services/transation.service';
import { Transation } from '../../models/transation.model';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginResponse } from '../../models/loginResponse.model';

@Component({
  selector: 'app-user-dashboard',
  imports: [SummaryComponent, FormsModule],
  templateUrl: './user-dashboard.component.html',
  styleUrl: './user-dashboard.component.css',
})
export class UserDashboardComponent {
  constructor(
    private transationService: TransationService,
    private router: Router
  ) {
    //login request from loginPage
    const navigation = this.router.getCurrentNavigation();
    this.loginResponse = (
      navigation?.extras?.state as { data: LoginResponse }
    )?.data;
  }

  //login request
  loginResponse: LoginResponse = {
    name: '',
    token: '',
    userId: '',
  };

  //Page request
  page = 1;
  limit = 30;

  //Transation data
  transations: Transation[] = [];
  description = '';
  value = 0;
  options = [
    { value: 'option1', label: 'Entrada' },
    { value: 'option2', label: 'Saída' },
  ];
  selectedoption: any;

  //format date
  currentDate: Date = new Date('25/03/20');

  get createTransation(): Transation {
    return {
      description: this.description,
      value: this.value,
      status: this.selectedoption,
      personId: this.loginResponse.userId,
      date: this.currentDate,
    };
  }

  ngOnInit() {
    this.getTransations();
  }

  postTransations() {
    this.transationService.createTransation(this.createTransation).subscribe(
      (response) => this.getTransations(),
      (error: any) => console.log(error)
    );
  }

  getTransations() {
    this.transationService
      .getTransation(this.loginResponse.userId, this.page, this.limit)
      .subscribe((transations) => {
        this.transations = transations || [];
      });
  }
}
