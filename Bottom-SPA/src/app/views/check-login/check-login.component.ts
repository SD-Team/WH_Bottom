import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../_core/_services/auth.service';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-check-login',
  templateUrl: './check-login.component.html',
  styleUrls: ['./check-login.component.scss']
})
export class CheckLoginComponent implements OnInit {

  constructor(private authService: AuthService,
    private router: Router,
    private spinner: NgxSpinnerService) {

  }

  async ngOnInit() {
    this.spinner.show();
    const returnUrl = this.router.url;
    if (returnUrl.includes('?account=')) {
      let username = '';
      username = returnUrl.substring(returnUrl.lastIndexOf('?account=') + 9);
      await this.authService.checkLogin(username).catch(err => {
        window.location.href = 'https://10.4.0.48:8001/';
      });
    }
    if (this.authService.loggedIn()) {
      this.router.navigate(['/dashboard']);
    }
    else {
      window.location.href = 'https://10.4.0.48:8001/';
    }
  }
}
