import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MaterialSheetSize } from '../../../_core/_models/material-sheet-size';
import { OutputService } from '../../../_core/_services/output.service';
import { TransferService } from '../../../_core/_services/transfer.service';
import { TransferDetail } from '../../../_core/_models/transfer-detail';
import { OutputM } from '../../../_core/_models/outputM';
import { FunctionUtility } from '../../../_core/_utility/function-utility';
import { AlertifyService } from '../../../_core/_services/alertify.service';

@Component({
  selector: 'app-output-process',
  templateUrl: './output-process.component.html',
  styleUrls: ['./output-process.component.scss'],
})
export class OutputProcessComponent implements OnInit {
  transactionDetails: TransferDetail[] = [];
  result1 = []; // là listmaterialsheetsize sau khi group by theo toolsize, vì lúc hiện theo toolsize
  result2 = []; // là transactiondetail sau khi group by theo toolsize, vì lúc hiện theo toolsize
  // tslint:disable-next-line: max-line-length
  result3 = []; // mảng chứa số lượng cần output ra theo từng size: là mảng để so sánh result1 và result2 xem ai nhỏ hơn thì lấy, và result 3 có thể thay đổi được nên tách ra thêm mảng nữa
  output: any = [];

  constructor(
    private router: Router,
    private outputService: OutputService,
    private transferService: TransferService,
    private route: ActivatedRoute,
    private functionUtility: FunctionUtility,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    // lấy ra currenoutput lưu trong oututservice, khi từ output main qua là có gán currentoutput
    this.outputService.currentOutputM.subscribe((res) => {
      this.output = res;
    });

    // lấy ra materialsheetsize: số lượng xuất ra theo đơn: lưu trong output service lúc load main lên là có lưu
    this.outputService.currentListMaterialSheetSize.subscribe((res) => {
      this.result1 = res;
    });

    this.getData();
  }

  back() {
    this.router.navigate(['output/main']);
  }
  changeInput(e, i) {
    // khi thay đổi giá trị input thì bắt ràng buộc
    const tmp = this.result1[i].value > this.result2[i].value ? this.result2[i].value : this.result1[i].value;
    if (e > tmp) {
      const ele = document.getElementById('id-' + i) as HTMLInputElement;
      ele.value = tmp;
      this.result3[i].value = tmp;
    }
    if (e < 0) {
      const ele = document.getElementById('id-' + i) as HTMLInputElement;
      ele.value = '0';
      this.result3[i].value = 0;
    }
  }
  getData() {
    // lấy ra transaction detail dựa vào transaction main
    this.transferService
      .getTransferDetail(this.output.transacNo)
      .subscribe((res) => {
        this.transactionDetails = res;

        // Group by transactiondetail theo tool_Size rồi gán vào result2
        const groups = new Set(
            this.transactionDetails.map((item) => item.tool_Size)
          ),
          results = [];
        groups.forEach((g) =>
          results.push({
            name: g,
            value: this.transactionDetails
              .filter((i) => i.tool_Size === g)
              .reduce((instock_Qty, j) => {
                return (instock_Qty += j.instock_Qty);
              }, 0),
            array: this.transactionDetails.filter((i) => i.tool_Size === g),
          })
        );
        this.result2 = results;

        // chạy từng phần tử trong result1 và result2 để so sánh phần tử nào nhỏ hơn thì lấy phần tử đó gán vào result3: 
        // result1 và result2 có cùng độ dài và result3 cũng bằng độ dài
        for (let i = 0; i < this.result1.length; i++) {
          this.result3.push({
            value:
              this.result1[i].value > this.result2[i].value
                ? this.result2[i].value
                : this.result1[i].value,
            name: this.result1[i].name,
          });
        }
      });
  }

  save() {
    //// -------- lúc lưu thì lấy biến listoutputmain lưu trên outputservice rồi gán giá trị mới của outputmain mới lưu
    let listOutputM: OutputM[];
    this.outputService.currentListOutputM.subscribe(
      (res) => (listOutputM = res)
    );
    // lấy ra vị trí của outputmain vừa lưu
    const indexOutput = listOutputM.indexOf(this.output);

    // sinh ra transacno mới theo yêu cầu viết trong hàm tiện ích
    this.output.transacNo = this.functionUtility.getOutSheetNo(
      this.output.planNo
    );
    // gán giá trị transoutqty mới bằng tổng số lượng đã output ra trong result3
    this.output.transOutQty = this.result3.reduce((value, i) => {
      return (value += i.value);
    }, 0);
    this.output.remainingQty = this.output.inStockQty - this.output.transOutQty;
    // gán pickupno bằng giá trị sheetno trong input lúc scan: mình đặt tên biến ra qrcodeId, có lưu trong outputservice để dùng chung
    this.outputService.currentQrCodeId.subscribe((res) => {
      this.output.pickupNo = res;
    });

    // thay thế outputmain cũ thành giá trị mới để lúc save quay về trang main hiện giá trị sau lúc save
    if (indexOutput !== -1) {
      listOutputM[indexOutput] = this.output;
    }
    //// --------

    // thay đổi giá trị result1 sau khi output lần đầu nếu output chưa hết bằng giá trị cũ trừ cho giá trị đã output ra
    this.result1.forEach((element, i) => {
      element.value = element.value - this.result3[i].value;
    });
    this.outputService.changeListMaterialSheetSize(this.result1);

    //// -------- tạo biến lưu danh sách transactiondetail có giá trị thay đổi mới sau khi output để gửi lên server lưu db
    const tmpTranssactionDetails = [];
    this.result3.forEach((i) => {
      this.result2.forEach((j) => {
        if (i.name === j.name) {
          j.array.forEach((k) => {
            if (i.value > k.instock_Qty) {
              const tmpInstock_Qty = k.instock_Qty;
              k.instock_Qty = 0;
              i.value = i.value - tmpInstock_Qty;
              k.qty = k.trans_Qty;
              k.trans_Qty = tmpInstock_Qty;
            } else if (i.value !== 0) {
              k.instock_Qty = k.instock_Qty - i.value;
              k.qty = k.trans_Qty;
              k.trans_Qty = i.value;
              i.value = 0;
            }
            tmpTranssactionDetails.push(k);
          });
        }
      });
    });

    // gửi lên server với output và transactiondetail dựa vào output có giá trị mới
    this.outputService
      .saveOutput(this.output, tmpTranssactionDetails)
      .subscribe(
        () => {
          this.alertify.success('Save succeed');
        },
        (error) => {
          this.alertify.error(error);
        }
      );

    //// --------

    // lưu lại những biến dùng chung ở outputservice rồi chuyển lại trang main
    this.outputService.changeListOutputM(listOutputM);
    this.outputService.changeFlagFinish(true);
    this.router.navigate(['/output/main']);
  }
}
