import { Component, OnInit } from '@angular/core';
import { RackLocation } from '../../../_core/_models/rack-location';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-rack-form',
  templateUrl: './rack-form.component.html',
  styleUrls: ['./rack-form.component.scss']
})
export class RackFormComponent implements OnInit {
  rack: any = {};
  constructor(
    private alertify: AlertifyService,
    private router: Router) { }

  ngOnInit() {
  }

  backList() {
    this.router.navigate(["/rack/main"])
  }

}
