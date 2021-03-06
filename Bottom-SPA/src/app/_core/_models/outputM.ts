import { TransferDetail } from './transfer-detail';

export interface OutputM {
    transacNo: string;
    qrCodeId: string;
    qrCodeVersion: number;
    planNo: string;
    supplierNo: string;
    supplierName: string;
    batch: string;
    matId: string;
    matName: string;
    wh: string;
    building: string;
    area: string;
    rackLocation: string;
    inStockQty: number;
    transOutQty: number;
    remainingQty: number;
    pickupNo: string;
}

export interface OutputParam {
    output: OutputM;
    transactionDetail: TransferDetail[];
}
