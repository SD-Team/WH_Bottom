import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-record-form-batches',
  templateUrl: './record-form-batches.component.html',
  styleUrls: ['./record-form-batches.component.scss']
})
export class RecordFormBatchesComponent implements OnInit {
  type: string = 'Batches';
  constructor(private router: Router) { }

  ngOnInit() {
  }
  changeForm() {
    if (this.type === 'No Batch') {
      this.router.navigate(['/receipt/record/add']);
    }
  }
  backForm() {
    this.router.navigate(['/receipt/record']);
  }
}
