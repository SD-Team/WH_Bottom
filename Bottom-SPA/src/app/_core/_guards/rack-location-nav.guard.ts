import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { CanActivate, Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class RackLocationNavNavGuard {
    user: any = JSON.parse(localStorage.getItem('user'));
    constructor(private authService: AuthService, private router: Router) { }
    canActivate(): boolean {
        if (this.user.role.includes('wmsb.RackLocationMain')) {
            return true;
        }
        else {
            this.router.navigate(['/dashboard']);
        }
    }
}
