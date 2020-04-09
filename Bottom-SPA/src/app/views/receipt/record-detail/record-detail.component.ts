import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReceiveNoDetail } from '../../../_core/_viewmodels/receive-no-detail';
import { MaterialService } from '../../../_core/_services/material.service';

@Component({
  selector: 'app-record-detail',
  templateUrl: './record-detail.component.html',
  styleUrls: ['./record-detail.component.scss']
})
export class RecordDetailComponent implements OnInit {
  receiveDetail: ReceiveNoDetail[] = [];
  constructor(private router: Router,
              private materialService: MaterialService) { }

  ngOnInit() {
    this.materialService.currentReceiveNoDetail.subscribe(res => this.receiveDetail = res );
  }
  backForm() {
    this.router.navigate(['receipt/record/']);
  }
}
