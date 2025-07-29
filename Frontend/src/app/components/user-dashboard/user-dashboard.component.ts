import { Component, ViewChild } from '@angular/core';
import { SummaryComponent } from '../summary/summary.component';
import { TransationService } from '../../services/transation.service';
import { Transation } from '../../models/transation.model';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginResponse } from '../../models/loginResponse.model';
import { NgxCurrencyDirective } from 'ngx-currency';
import { DeleteTransationComponent } from '../modals/delete-transation/delete-transation.component';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-user-dashboard',
  imports: [
    SummaryComponent,
    DeleteTransationComponent,
    FormsModule,
    NgxCurrencyDirective,
  ],
  templateUrl: './user-dashboard.component.html',
  styleUrl: './user-dashboard.component.css',
})
export class UserDashboardComponent {
  //modal
  @ViewChild('modal') modal!: DeleteTransationComponent;

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

  //delete transation
  transationtoDelete?: Transation;

  //button action
  action = '+';

  //action message
  actionMessage: string = '';

  //login request
  loginResponse: LoginResponse = {
    name: '',
    token: '',
    userId: '',
  };

  //token
  tokenObj: HttpHeaders = new HttpHeaders();

  //Page request
  page = 1;
  limit = 30;

  //Order date
  orderDate: string = '▾';

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

  //checkInputs status
  inputStatus = 'true';

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
    this.getToken();
    this.getTransations();
  }

  //get token
  getToken() {
    this.tokenObj = new HttpHeaders().set(
      'Authorization',
      'bearer ' + this.loginResponse.token
    );
  }

  //check inputs before post/patch
  async checkInputs() {
    if (
      this.value == 0 ||
      this.description == '' ||
      this.selectedoption == ''
    ) {
      this.inputStatus = 'false';
      this.actionMessage = 'Todos os campos são obrigatórios!';
      await this.delay(3000);
    }
    this.inputStatus = 'true';
    this.actionMessage = '';
  }

  postTransations() {
    this.checkInputs();
    if (this.inputStatus == 'false') {
    } else {
      this.transationService
        .createTransation(this.createTransation, this.tokenObj)
        .subscribe(
          (response) => {
            this.getTransations();
            this.resetInputs();
            this.showActionMessage('post');
            console.log(this.createTransation);
          },
          (error: any) => console.log(error)
        );
    }
  }

  getTransations() {
    this.transationService
      .getTransation(
        this.loginResponse.userId,
        this.page,
        this.limit,
        this.tokenObj
      )
      .subscribe((transations) => {
        this.transations = this.transationAscDate(transations) || [];
        console.log(transations[0].date);
      });
  }

  patchTransation() {
    this.checkInputs();
    if (this.inputStatus == 'false') {
    } else {
      this.transationService
        .patchTransation(this.createTransation, this.patchId!, this.tokenObj)
        .subscribe((response) => {
          this.getTransations();
          this.resetInputs();
          this.showActionMessage('patch');
          this.action = '+';
        });
    }
  }

  deleteTransation(transation: Transation) {
    this.transationService
      .deleteTransation(transation.id!, this.tokenObj)
      .subscribe(
        (response) => {
          console.log('Exclusão do item: ' + transation.description);
          this.getTransations();
          this.showActionMessage('delete');
        },
        (error) => console.log(error)
      );
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

  openModal(transation: Transation) {
    this.modal.showModal();
    this.transationtoDelete = transation;
  }

  transationAscDate(transations: Transation[]) {
    transations.sort(
      (a, b) => new Date(a.date).getTime() - new Date(b.date).getTime()
    );
    if (this.orderDate == '▵') this.orderDate = '▾';
    return transations;
  }

  transationDescDate(transations: Transation[]) {
    this.orderDate = '▵';
    transations.sort(
      (a, b) => new Date(b.date!).getTime() - new Date(a.date!).getTime()
    );
    return transations;
  }

  async showActionMessage(route: string) {
    if (route == 'post') {
      this.actionMessage = 'Transação adicionada com sucesso!';
      await this.delay(3000);
    }
    if (route == 'patch') {
      this.actionMessage = 'Transação atualizada com sucesso!';
      await this.delay(3000);
    }
    if (route == 'delete') {
      this.actionMessage = 'Transação deletada com sucesso!';
      await this.delay(3000);
    }

    this.actionMessage = '';
  }

  //function for timeout
  delay(ms: number) {
    return new Promise((resolve) => setTimeout(resolve, ms));
  }
}
