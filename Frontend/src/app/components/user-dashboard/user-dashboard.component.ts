import { Component } from '@angular/core';
import { SummaryComponent } from '../summary/summary.component';
import { TransationService } from '../../services/transation.service';
import { Transation } from '../../models/transation.model';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginResponse } from '../../models/loginResponse.model';
import { NgxCurrencyDirective } from 'ngx-currency';

@Component({
  selector: 'app-user-dashboard',
  imports: [SummaryComponent, FormsModule, NgxCurrencyDirective],
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

  //
  action = '+';

  //login request
  loginResponse: LoginResponse = {
    name: '',
    token: '',
    userId: '',
  };

  teste = 'testeee';

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
  currentDate: any = new Date().toISOString().split('T')[0];

  //patch id transation
  patchId: string = '';

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
      (response) => {
        this.getTransations();
        this.resetInputs();
        console.log(this.createTransation.value);
      },
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

  deleteTransation(transation: Transation) {
    this.transationService.deleteTransation(transation.id!).subscribe(
      (response) => {
        console.log('Exclusão do item: ' + transation.description);
        this.getTransations();
      },
      (error) => console.log(error)
    );
  }

  patchTransation() {
    this.transationService
      .patchTransation(this.createTransation, this.patchId!)
      .subscribe((response) => {
        this.getTransations();
        this.resetInputs();
        this.action = '+';
      });
  }

  editButton(transation: Transation) {
    this.description = transation.description;
    this.value = transation.value;
    this.selectedoption = transation.status;
    this.currentDate = transation.editDate!;

    //change action button value
    this.action = 'Editar';
    this.patchId = transation.id!;
  }

  resetInputs() {
    this.description = '';
    this.value = 0;
    this.selectedoption = '';
    this.currentDate = new Date().toISOString().split('T')[0];
  }
}
