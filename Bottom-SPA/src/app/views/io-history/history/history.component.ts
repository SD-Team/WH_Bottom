import { Component, OnInit } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { History } from '../../../_core/_models/history';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss']
})
export class HistoryComponent implements OnInit {
  bsConfig: Partial<BsDatepickerConfig>;

  fromDate = new Date();
  toDate = new Date();
  results = [];
  constructor() { }

  ngOnInit() {
    this.bsConfig = Object.assign({}, { containerClass: 'theme-blue' });
  }

  search() {
    const a = new History();
    a.date = '2020-02-25 00:00:00.000';
    a.status = 'in';
    a.plan_no = '0124696503';
    a.destination_location = 'RW19c00QCY';
    a.batch = -1;
    a.mat_name = 'SFCD27W00A';
    a.source_location = 'A-01-02';
    a.destination_location = 'C-03-10';
    a.inOut_no = 'RW19C00QYC';
    this.results.push(a);
    const b = new History();
    b.date = '2020-02-25 00:00:00.000';
    b.status = 'out';
    b.plan_no = '0124696503';
    b.destination_location = 'RW19c00QCY';
    b.batch = -1;
    b.mat_name = 'SFCD27W00A';
    b.source_location = 'A-01-02';
    b.destination_location = 'C-03-10';
    b.inOut_no = 'RW19C00QYC';
    this.results.push(b);
  }

}
