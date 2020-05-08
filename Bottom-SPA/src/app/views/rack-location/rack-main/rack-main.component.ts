import { Component, OnInit } from '@angular/core';
import { RackService } from '../../../_core/_services/rack.service';
import { WmsCode } from '../../../_core/_models/wms-code';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { FilerRackParam } from '../../../_core/_models/filer-rack-param';
import { Pagination, PaginatedResult } from '../../../_core/_models/pagination';
import { Route } from '@angular/compiler/src/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RackLocation } from '../../../_core/_models/rack-location';
import { InputService } from '../../../_core/_services/input.service';

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
    factory: "C",
    wh: "C4",
    building: "",
    floor: "",
    area: ""
  };
  printArray: any = [];
  constructor(
    private rackServcie: RackService,
    private alertify: AlertifyService,
    private inputService: InputService,
    private route: ActivatedRoute,
    private router: Router) {}

  ngOnInit() {
    this.loadDatas();
    this.route.data.subscribe(data => {
      this.rackLocations = data['racks'].result;
      this.pagination = data['racks'].pagination;
    });
    this.inputService.changeListInputMain([]);
    this.inputService.changeFlag('');
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
  
  changeParams() {
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
    this.rackServcie.changeFlag("0");
    this.router.navigate(["/rack/form"])
  }

  changeToEdit(rack: RackLocation) {
    this.rackServcie.changeRack(rack);
    this.rackServcie.changeFlag("1");
    this.router.navigate(["/rack/form"]);
  }

  delete(rack: RackLocation) {
    this.alertify.confirm('Delete Rack Location', 'Are you sure you want to delete this rack location "' + rack.rack_Location + '" ?', () => {
      this.rackServcie.delete(rack.id).subscribe(() => {
        this.filter();
        this.alertify.success('Rack location has been deleted');
      }, error => {
        this.alertify.error('Failed to delete the rack location');
      });
    });
  }

  checkAll(e) {
    this.printArray = [];
    if (e.target.checked) {
      this.rackLocations.forEach(element => {
        let ele = document.getElementById(element.id.toString()) as HTMLInputElement;
        ele.checked = true;
        this.printArray.push(element.rack_Location);
      });
    } else {
      this.rackLocations.forEach(element => {
        let ele = document.getElementById(element.id.toString()) as HTMLInputElement;
        ele.checked = false;
      });
    }
    console.log(">>", this.printArray);
  }

  checkEle(e) {
    if (e.target.checked) {
      this.printArray.push(e.target.value);
    } else {
      let i = this.printArray.findIndex(element => element === e.target.value);
      console.log(i);
      this.printArray.splice(i, 1);
    }
    let ele = document.getElementById("all") as HTMLInputElement;
    if (this.printArray.length === this.rackLocations.length) {
      ele.checked = true;
    }else ele.checked = false;
  }

  print(rack: RackLocation) {
    this.printArray = [];
    this.printArray.push(rack.rack_Location);
    this.rackServcie.changeArr(this.printArray);
    this.router.navigate(["/rack/print"])
  }

  generateArr() {
    if (this.printArray.length > 0) {
      this.rackServcie.changeArr(this.printArray);
      this.router.navigate(["/rack/print"]);
    } else {
      this.alertify.error("Please choose Rack Location!");
    }
    
  }
}
