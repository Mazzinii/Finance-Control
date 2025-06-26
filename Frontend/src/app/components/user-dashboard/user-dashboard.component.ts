import { Component } from '@angular/core';
import { SummaryComponent } from '../summary/summary.component';

@Component({
  selector: 'app-user-dashboard',
  imports: [SummaryComponent],
  templateUrl: './user-dashboard.component.html',
  styleUrl: './user-dashboard.component.css',
})
export class UserDashboardComponent {}
