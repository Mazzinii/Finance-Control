import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Transation } from '../../models/transation.model';
import { CommonModule } from '@angular/common';
import { NgxEchartsDirective, provideEchartsCore } from 'ngx-echarts';
import * as echarts from 'echarts/core';
import { BarChart } from 'echarts/charts';
import { GridComponent } from 'echarts/components';
import { CanvasRenderer } from 'echarts/renderers';
import { EChartsCoreOption } from 'echarts/core';
echarts.use([BarChart, GridComponent, CanvasRenderer]);

@Component({
  selector: 'app-summary',
  imports: [CommonModule, NgxEchartsDirective],
  templateUrl: './summary.component.html',
  styleUrl: './summary.component.css',
  providers: [provideEchartsCore({ echarts })],
})
export class SummaryComponent {
  ngOnChanges(changes: SimpleChanges) {
    if (changes['transations']) {
      this.transationCalc();
    }
  }

  @Input() transations: Transation[] = [];

  // Pegar as os meses que são diferentes e escrever por extenso
  dates: [] = [];
  // Pegar as entradas no mes específico
  // Pegar as saidas no mes específico
  // separar dados por dicionario Ex Mes[TotalEntradas,TotalSaidas ]
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

  // Options for cards
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

  // Graphics
  chartOption: EChartsCoreOption = {
    xAxis: {
      type: 'category',
      data: this.transations.length,
    },
    yAxis: {
      type: 'value',
    },
    series: [
      {
        name: 'Vendas',
        data: [820, 932, 901],
        type: 'bar',
      },
    ],
  };
}
