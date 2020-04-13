import { Component, OnInit } from '@angular/core';
import { TransferDetail } from '../../../_core/_models/transfer-detail';
import { ActivatedRoute, Router } from '@angular/router';
import { TransferService } from '../../../_core/_services/transfer.service';

@Component({
  selector: 'app-transfer-detail',
  templateUrl: './transfer-detail.component.html',
  styleUrls: ['./transfer-detail.component.scss']
})
export class TransferDetailComponent implements OnInit {
  transferDetails: TransferDetail[] = [];
  transferType: string = '';
  transferNo: string = '';

  constructor(private router: Router, private route: ActivatedRoute,
    private transferService: TransferService) { }

  ngOnInit() {
    this.transferType = this.route.snapshot.params['transferType'];
    this.transferNo = this.route.snapshot.params['transferNo'];
    this.getData();
  }

  back() {
    this.router.navigate(['/transfer/history']);
  }

  getData() {
    this.transferService.getTransferDetail(this.transferNo).subscribe(res => {
      this.transferDetails = res;
    });
  }
}
