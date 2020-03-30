import { Component, OnInit } from '@angular/core';
import { RackService } from '../../../_core/_services/rack.service';

@Component({
  selector: 'app-rack-print',
  templateUrl: './rack-print.component.html',
  styleUrls: ['./rack-print.component.scss']
})
export class RackPrintComponent implements OnInit {
  elementType: 'url' | 'canvas' | 'img' = 'url';
  rackItem: any = [];
  constructor(private rackServcie: RackService) { }

  ngOnInit() {
    this.rackServcie.currentArr.subscribe(rackItem => this.rackItem = rackItem)
    this.rackItem.push("A-01-01-02");
    this.rackItem.push("A-01-01");
  }

  print() {
    let printContents = document.getElementById("wrap-print").innerHTML;
    let originalContents = document.body.innerHTML;
    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
  }

}
