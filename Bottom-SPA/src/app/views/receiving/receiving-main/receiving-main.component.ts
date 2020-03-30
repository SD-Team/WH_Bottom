import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-receiving-main',
  templateUrl: './receiving-main.component.html',
  styleUrls: ['./receiving-main.component.scss']
})
export class ReceivingMainComponent implements OnInit {
  toDate: Date;
  fromDate: Date;
  time_start: string;
  time_end: string;
  constructor() { }

  ngOnInit() {
  }
  search() {
    
  }
}
