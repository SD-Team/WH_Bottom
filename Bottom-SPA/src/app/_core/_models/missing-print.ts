import { Material } from './material';
import { TransferDetail } from './transfer-detail';

export class MissingPrint {
    materialMissing: Material;
    transactionDetailByMissingNo: TransferDetail[];
}
