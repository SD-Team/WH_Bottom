import { Component, OnInit } from '@angular/core';
import { RackService } from '../../../_core/_services/rack.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-rack-print',
  templateUrl: './rack-print.component.html',
  styleUrls: ['./rack-print.component.scss']
})
export class RackPrintComponent implements OnInit {
  elementType: 'url' | 'canvas' | 'img' = 'url';
  rackItem: any = [];
  constructor(private rackServcie: RackService, private router: Router) { }

  ngOnInit() {
    this.rackServcie.currentArr.subscribe(rackItem => this.rackItem = rackItem)
    
  }

  print(e) {
    e.preventDefault();
    let printContents = document.getElementById("wrap-print").innerHTML;
    let originalContents = document.body.innerHTML;
    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
  }

  back() {
    this.router.navigate(["/rack/main"])
  }

}
