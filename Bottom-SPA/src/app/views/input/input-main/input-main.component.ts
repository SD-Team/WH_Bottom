import { Component, OnInit, Input } from '@angular/core';
import { InputM } from '../../../_core/_models/inputM';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { InputService } from '../../../_core/_services/input.service';

@Component({
  selector: 'app-input-main',
  templateUrl: './input-main.component.html',
  styleUrls: ['./input-main.component.scss']
})
export class InputMainComponent implements OnInit {
  result: InputM[];
  qrCodeID = "";
  constructor(private inputService: InputService,
    private alertify: AlertifyService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.result = [];
  }


  getInputMain(e) {
    console.log(e.length);
    if (e.length === 14) {
      let flag = true;
      this.result.forEach(item => {
        if (item.qrCode_Id === e)
          flag = false;
      });
      if (flag) {
        this.inputService.getMainByQrCodeID(this.qrCodeID)
          .subscribe((res) => {
            if (res != null) 
              this.result.push(res);
          }, error => {
            this.alertify.error(error);
          });
      } else
        this.alertify.error("This QRCode scanded!");
      this.qrCodeID = ""
    }
  }

}
