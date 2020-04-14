import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class FunctionUtility {
    /**
     *Hàm tiện ích
     */


    constructor() {
    }

    /**
     *Trả ra ngày hiện tại, chỉ lấy năm tháng ngày: yyyy/MM/dd
     */
    getToDay() {
        const toDay = new Date().getFullYear().toString() +
            '/' + (new Date().getMonth() + 1).toString() +
            '/' + new Date().getDate().toString();
        return toDay;
    }

    /**
     *Trả ra transferNo mới theo yêu cầu: TB(ngày thực hiện yyyymmdd) 3 mã số random number. (VD: TB20200310001)
     */
    getTransferNo() {
        const transferNo =
            'TB' +
            new Date().getFullYear().toString() +
            (new Date().getMonth() + 1).toString() +
            new Date().getDate().toString() +
            Math.floor(Math.random() * (999 - 100 + 1) + 100);
        return transferNo;
    }

    /**
     *Trả ra outputSheetNo mới theo yêu cầu: TB(ngày thực hiện yyyymmdd) 3 mã số random number. (VD: TB20200310001)
     */
    getOutSheetNo() {
        const outputSheetNo =
            'OB' +
            new Date().getFullYear().toString() +
            (new Date().getMonth() + 1).toString() +
            new Date().getDate().toString() +
            Math.floor(Math.random() * (999 - 100 + 1) + 100);
        return outputSheetNo;
    }
}
