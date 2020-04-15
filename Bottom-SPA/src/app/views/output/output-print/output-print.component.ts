import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-output-print',
  templateUrl: './output-print.component.html',
  styleUrls: ['./output-print.component.scss']
})
export class OutputPrintComponent implements OnInit {
  elementType: 'url' | 'canvas' | 'img' = 'url';
  printOutput = 'Output test';
  today = new Date();

  constructor(private router: Router) { }

  ngOnInit() {
  }

  print(e) {
    e.preventDefault();
    const printContents = document.getElementById('wrap-print').innerHTML;
    const originalContents = document.body.innerHTML;
    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
  }

  back() {
    this.router.navigate(['output/main']);
  }

}
