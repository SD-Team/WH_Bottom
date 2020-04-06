import { OrderSizeAccumlate } from './ordersize-accumlate';
export interface BatchQtyItem {
    mO_Seq: string;
    purchase_No: string;
    missing_No: string;
    purchase_Qty: OrderSizeAccumlate[];
}