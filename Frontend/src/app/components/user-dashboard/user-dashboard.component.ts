import { Component, OnInit } from '@angular/core';
import { SummaryComponent } from '../summary/summary.component';
import { TransationService } from '../../services/transation.service';
import { Router, RouterLink } from '@angular/router';
import { Transation } from '../../models/transation.model';
import { FormsModule } from '@angular/forms';
import { response } from 'express';

@Component({
  selector: 'app-user-dashboard',
  imports: [SummaryComponent, RouterLink, FormsModule],
  templateUrl: './user-dashboard.component.html',
  styleUrl: './user-dashboard.component.css',
})
export class UserDashboardComponent {
  personId = '6ebb4d2b-dc70-4d0e-852b-9c5639494755';
  page = 1;
  limit = 10;

  description = '';
  value = 0;
  options = [
    { value: 'option1', label: 'Entrada' },
    { value: 'option2', label: 'Saída' },
  ];
  selectedoption: any;
  date: Date = new Date();

  get createTransation(): Transation {
    return {
      description: this.description,
      value: this.value,
      status: this.selectedoption,
      personId: this.personId,
      date: this.date,
    };
  }

  transations: Transation[] = [];

  constructor(
    private transationService: TransationService,
    private router: Router
  ) {}

  ngOnInit() {
    this.getTransations();
  }

  //verificar o pq nao esta atualiando a pagina
  createUpdatePage() {
    this.postTransations();
    this.getTransations();
  }

  postTransations() {
    this.transationService.createTransation(this.createTransation).subscribe(
      (response) => console.log(this.createTransation),
      (error: any) => console.log(error)
    );
  }

  getTransations() {
    this.transationService
      .getTransation(this.personId, this.page, this.limit)
      .subscribe((transations) => {
        this.transations = transations || [];
        console.log(this.transations[0]);
      });
  }
}
