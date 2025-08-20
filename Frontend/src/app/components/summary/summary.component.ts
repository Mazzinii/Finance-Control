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
  options: number = 1;
  options1 = 'selected';
  options2 = '';

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
    if (this.balance > 0) this.balanceStatus = 'values green';
    if (this.balance == 0) this.balanceStatus = 'values';
    if (this.balance < 0) this.balanceStatus = 'values red';
  }

  changeOption(optionNumber: number) {
    if (optionNumber == 1) {
      this.options = 1;
      this.options1 = 'selected';
      this.options2 = '';
    }
    if (optionNumber == 2) {
      this.options = 2;
      this.options2 = 'selected';
      this.options1 = '';
    }
  }
}
