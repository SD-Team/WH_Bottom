import { Component, OnInit, OnDestroy } from '@angular/core';
import { RackLocation } from '../../../_core/_models/rack-location';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { Router } from '@angular/router';
import { WmsCode } from '../../../_core/_models/wms-code';
import { RackService } from '../../../_core/_services/rack.service';

@Component({
  selector: 'app-rack-form',
  templateUrl: './rack-form.component.html',
  styleUrls: ['./rack-form.component.scss']
})
export class RackFormComponent implements OnInit, OnDestroy {
  rack: any = {};
  flag = "0";
  factories: WmsCode[];
  whs: WmsCode[];
  buildings: WmsCode[];
  floors: WmsCode[];
  areas: WmsCode[];
  constructor(
    private rackServcie: RackService,
    private alertify: AlertifyService,
    private router: Router) { }

  ngOnDestroy(): void {
    // khi component kết thúc thì gán rack trên service thành giá trị mặc định ban đầu là rỗng,
    // để khi mình edit xong mà thêm mới lại ko bị lỗi
    const rack: RackLocation = {
      id: 0,
      rack_Location: '',
      factory_ID: '',
      wH_ID: '',
      build_ID: '',
      floor_ID: '',
      area_ID: '',
      rack_Code: '',
      rack_Level: '',
      rack_Bin: '',
      cBM: 0,
      max_per: 0,
      memo: '',
      rack_Invalid_date: new Date(),
      updated_Time: new Date(),
      updated_By: '',
      buildingName: '',
      areaName: '',
      floorName: '',
    };
    this.rackServcie.changeRack(rack);
  }

  ngOnInit() {
    this.loadDatas();
    this.rackServcie.currentRack.subscribe(rack => this.rack = rack);
    this.rackServcie.currentFlag.subscribe(flag => this.flag = flag);
    if (this.flag === "0") {
      this.rack.factory_ID = "C";
      this.rack.area_ID = "";
      this.rack.wH_ID = "C4";
      this.rack.floor_ID = "";
      this.rack.build_ID = "";
    }
  }

  create() {
    if (this.rack.factory_ID === "" || this.rack.area_ID === "" || this.rack.wH_ID === "" ||
      this.rack.floor_ID === "" || this.rack.build_ID === "" || this.rack.rack_Location === "") {
      this.alertify.error("Please enter full information!");
    } else {
      delete this.rack.updated_Time;
      delete this.rack.rack_Invalid_date;
      delete this.rack.cBM;
      delete this.rack.max_per;
      this.rackServcie.create(this.rack).subscribe(
        () => {
          this.alertify.success("Add succeed");
          this.router.navigate(["/rack/main"]);
        },
        error => {
          this.alertify.error(error);
        }
      )
    }
  }

  update() {
    this.rackServcie.update(this.rack).subscribe(
      () => {
        this.alertify.success("Updated succeed");
        this.router.navigate(["/rack/main"]);
      },
      error => {
        this.alertify.error(error)
      }
    )
  }

  loadDatas() {
    this.rackServcie.getFactories()
      .subscribe((res) => {
        console.log(res);
        this.factories = res;
      }, error => {
        this.alertify.error(error);
      });
    this.rackServcie.getWHs()
      .subscribe((res) => {
        this.whs = res;
      }, error => {
        this.alertify.error(error);
      });
    this.rackServcie.getBuildings()
      .subscribe((res) => {
        this.buildings = res;
      }, error => {
        this.alertify.error(error);
      });
    this.rackServcie.getFloors()
      .subscribe((res) => {
        this.floors = res;
      }, error => {
        this.alertify.error(error);
      });
    this.rackServcie.getAreas()
      .subscribe((res) => {
        this.areas = res;
      }, error => {
        this.alertify.error(error);
      });
  }
  backList() {
    this.router.navigate(["/rack/main"]);
  }

}
