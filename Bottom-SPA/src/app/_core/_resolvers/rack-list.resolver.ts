import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { RackLocation } from '../_models/rack-location';
import { RackService } from '../_services/rack.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class RackListResolver implements Resolve<RackLocation[]> {
    pageNumber = 1;
    pageSize = 10;
    params: any = {
        factory: "",
        wh: "",
        building: "",
        floor: "",
        area: ""
    };
    constructor(private rackService: RackService, private router: Router, private alertify: AlertifyService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<RackLocation[]> {
        return this.rackService.filter(this.pageNumber, this.pageSize, this.params).pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/dashboard']);
                return of(null);
            }),
        );
    }
}
