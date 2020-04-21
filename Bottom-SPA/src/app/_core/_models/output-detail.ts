import { TransferDetail } from './transfer-detail';

export interface OutputDetail {
    id: number;
    planNo: string;
    batch: string;
    matId: string;
    matName: string;
    qrCodeId: string;
    transactionDetail: TransferDetail[];
}
