import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-record-detail',
  templateUrl: './record-detail.component.html',
  styleUrls: ['./record-detail.component.scss']
})
export class RecordDetailComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }
  backForm() {
    this.router.navigate(['receipt/record/']);
  }
}
