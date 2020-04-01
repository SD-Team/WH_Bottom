import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MaterialService } from '../../../_core/_services/material.service';
import { MaterialModel } from '../../../_core/_viewmodels/material-model';

@Component({
  selector: 'app-record-form',
  templateUrl: './record-form.component.html',
  styleUrls: ['./record-form.component.scss']
})
export class RecordFormComponent implements OnInit {
  fromDate = new Date();
  isDisabled = true;
  type: string = 'No Batch';
  materialModel: MaterialModel;
  constructor(private router: Router,
              private materialService: MaterialService) { }

  ngOnInit() {
    this.materialService.currentMaterial.subscribe(res => this.materialModel = res);
    console.log(this.materialModel);
  }
  changeForm() {
    if (this.type === 'Batches') {
      this.router.navigate(['/receipt/record/add-batches']);
    }
  }
  backForm() {
    this.router.navigate(['/receipt/record/']);
  }
}
