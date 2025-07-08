import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Transation } from '../../models/transation.model';

@Component({
  selector: 'app-summary',
  imports: [],
  templateUrl: './summary.component.html',
  styleUrl: './summary.component.css',
})
export class SummaryComponent {
  ngOnChanges(changes: SimpleChanges) {
    if (changes['transations']) {
      this.transationCalc();
    }
  }

  @Input() transations: Transation[] = [];

  entry: number = 0;
  exit: number = 0;
  balance: number = 0;
  balanceStatus = '';

  transationCalc() {
    this.entry = 0;
    this.exit = 0;

    for (let index = 0; index < this.transations.length; index++) {
      if (this.transations[index].status == 'Entrada') {
        this.entry += this.transations[index].value;
      }
      if (this.transations[index].status == 'Saída') {
        this.exit += this.transations[index].value;
      }
    }

    this.balance = this.entry - this.exit;
    this.balanceCheck();
  }

  balanceCheck() {
    if (this.balance > 0) this.balanceStatus = 'values-green';
    else this.balanceStatus = 'values-red';
  }
}
