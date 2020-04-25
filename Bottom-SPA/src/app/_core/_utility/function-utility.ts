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
     *Trả ra ngày với tham số truyền vào là ngày muốn format, chỉ lấy năm tháng ngày: yyyy/MM/dd
     */
    getDateFormat(day: Date) {
        const dateFormat = day.getFullYear().toString() +
            '/' + (day.getMonth() + 1).toString() +
            '/' + day.getDate().toString();
        return dateFormat;
    }

    /**
     *Trả ra transferNo mới theo yêu cầu: TB(ngày thực hiện yyyymmdd) 3 mã số random number. (VD: TB20200310001)
     */
    getTransferNo() {
        const transferNo =
            'TB' +
            new Date().getFullYear().toString() +
            ((new Date().getMonth() + 1 > 9) ? (new Date().getMonth() + 1).toString() : ('0' + (new Date().getMonth() + 1).toString())) +
            (new Date().getDate() > 9 ? new Date().getDate().toString() : '0' + new Date().getDate().toString()) +
            Math.floor(Math.random() * (999 - 100 + 1) + 100);
        return transferNo;
    }

    /**
     *Trả ra outputSheetNo mới theo yêu cầu: BO+(Plan No)+ 3 số random (001,002,003…). VD: BO0124696503001
     */
    getOutSheetNo(planNo: string) {
        const outputSheetNo =
            'BO' + planNo +
            Math.floor(Math.random() * (999 - 100 + 1) + 100);
        return outputSheetNo;
    }
}
