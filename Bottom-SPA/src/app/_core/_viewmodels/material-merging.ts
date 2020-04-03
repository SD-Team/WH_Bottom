import { BatchQtyItem } from './batch-qty-item';
export interface MaterialMergingViewModel {
    order_Size: string;
    purchase_Qty: number;
    purchase_Qty_Item: BatchQtyItem[]
}