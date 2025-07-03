import { Component, OnInit } from '@angular/core';
import { SummaryComponent } from '../summary/summary.component';
import { TransationService } from '../../services/transation.service';
import { Router, RouterLink } from '@angular/router';
import { Transation } from '../../models/transation.model';

@Component({
  selector: 'app-user-dashboard',
  imports: [SummaryComponent, RouterLink],
  templateUrl: './user-dashboard.component.html',
  styleUrl: './user-dashboard.component.css',
})
export class UserDashboardComponent {
  personId = '6ebb4d2b-dc70-4d0e-852b-9c5639494755';
  page = 1;
  limit = 10;

  transations: Transation[] = [];

  constructor(
    private transationService: TransationService,
    private router: Router
  ) {}
  ngOnInit() {
    this.getTransations();
  }

  getTransations() {
    var response = this.transationService
      .getTransation(this.personId, this.page, this.limit)
      .subscribe((transations) => {
        this.transations = transations || [];
        console.log(this.transations[0]);
      });
  }
}
