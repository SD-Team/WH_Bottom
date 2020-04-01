import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MaterialModel } from '../../../_core/_viewmodels/material-model';
import { MaterialService } from '../../../_core/_services/material.service';

@Component({
  selector: 'app-record',
  templateUrl: './record.component.html',
  styleUrls: ['./record.component.scss']
})
export class RecordComponent implements OnInit {
  materialModel: MaterialModel;
  constructor(private router: Router,
              private materialService: MaterialService) { }

  ngOnInit() {
    this.materialService.currentMaterial.subscribe(res => this.materialModel = res);
    console.log(this.materialModel);
  }
  changeForm() {
    this.router.navigate(['/receipt/record/add']);
  }
  backForm() {
    this.router.navigate(['/receipt/main/']);
  }
}
