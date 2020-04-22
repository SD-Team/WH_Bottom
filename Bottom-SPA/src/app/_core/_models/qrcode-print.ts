import { PackingList } from './packingList';
import { TransferDetail } from './transfer-detail';

export class QrcodePrint {
    transactionDetailByQrCodeId: TransferDetail[];
    packingListByQrCodeId: PackingList;
    rackLocation: string;
}