import { Component, OnInit } from '@angular/core';
import { RackService } from '../../../_core/_services/rack.service';
import { WmsCode } from '../../../_core/_models/wms-code';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { FilerRackParam } from '../../../_core/_models/filer-rack-param';
import { Pagination } from '../../../_core/_models/pagination';

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
  params: any = {
    factory: "",
    wh: "",
    building: "",
    floor: "",
    area: ""
  };
  constructor(private rackServcie: RackService, private alertify: AlertifyService) { 
    
  }

  ngOnInit() {
    this.loadFactories();
    this.loadWhs();
    this.loadBuildings();
    this.loadFloors();
    this.loadAreas();
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

  changeFactory() {
    console.log(this.params);
    this.filter();
  }

  changeWh() {
    this.filter();
  }

  changeBuilding() {
    this.filter();
  }

  changeFloor() {
    this.filter();
  }

  changeArea() {
    this.filter();
  }

  filter() {
    console.log("Call: ", this.params)
    this.rackServcie.filter(1, 3, this.params).subscribe(
      (res) => {
        console.log(res);
        this.alertify.success("Succeed");
      }
    )
  }
}
