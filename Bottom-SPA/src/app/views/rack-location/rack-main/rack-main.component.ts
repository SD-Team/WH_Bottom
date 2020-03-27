import { Component, OnInit } from '@angular/core';
import { RackService } from '../../../_core/_services/rack.service';
import { WmsCode } from '../../../_core/_models/wms-code';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { FilerRackParam } from '../../../_core/_models/filer-rack-param';
import { Pagination, PaginatedResult } from '../../../_core/_models/pagination';
import { Route } from '@angular/compiler/src/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RackLocation } from '../../../_core/_models/rack-location';

@Component({
  selector: 'app-rack-main',
  templateUrl: './rack-main.component.html',
  styleUrls: ['./rack-main.component.scss']
})
export class RackMainComponent implements OnInit {
  pagination: Pagination;
  factories: WmsCode[];
  whs: WmsCode[];
  buildings: WmsCode[];
  floors: WmsCode[];
  areas: WmsCode[];
  rackLocations: RackLocation[];
  params: any = {
    factory: "",
    wh: "",
    building: "",
    floor: "",
    area: ""
  };
  constructor(
    private rackServcie: RackService,
    private alertify: AlertifyService,
    private route: ActivatedRoute,
    private router: Router) { 
    
  }

  ngOnInit() {
    this.loadFactories();
    this.loadWhs();
    this.loadBuildings();
    this.loadFloors();
    this.loadAreas();
    this.route.data.subscribe(data => {
      this.rackLocations = data['racks'].result;
      this.pagination = data['racks'].pagination;
    });
    console.log(this.params);
  }
  
  loadFactories() {
    this.rackServcie.getFactories()
      .subscribe((res) => {
        console.log(res);
        this.factories = res;
      }, error => {
        this.alertify.error(error);
      });
  }

  loadWhs() {
    this.rackServcie.getWHs()
      .subscribe((res) => {
        this.whs = res;
      }, error => {
        this.alertify.error(error);
      });
  }

  loadBuildings() {
    this.rackServcie.getBuildings()
      .subscribe((res) => {
        this.buildings = res;
      }, error => {
        this.alertify.error(error);
      });
  }

  loadFloors() {
    this.rackServcie.getFloors()
      .subscribe((res) => {
        this.floors = res;
      }, error => {
        this.alertify.error(error);
      });
  }

  loadAreas() {
    this.rackServcie.getAreas()
      .subscribe((res) => {
        this.areas = res;
      }, error => {
        this.alertify.error(error);
      });
  }

  changeParams() {
    console.log(this.params);
    this.filter();
  }

  filter() {
    console.log("Call: ", this.params)
    this.rackServcie.filter(this.pagination.currentPage, this.pagination.itemsPerPage, this.params).subscribe(
      (res: PaginatedResult<RackLocation[]>) => {
        console.log(res);
        this.rackLocations = res.result;
        this.pagination = res.pagination;
        this.alertify.success("Succeed");
      }
    )
  }
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.filter();
  }

  changeToFrom() {
    this.router.navigate(["/rack/form"])
  }
  
}
