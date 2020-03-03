import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-record-form',
  templateUrl: './record-form.component.html',
  styleUrls: ['./record-form.component.scss']
})
export class RecordFormComponent implements OnInit {
  fromDate = new Date();
  isDisabled = true;
  constructor() { }

  ngOnInit() {
  }

}
