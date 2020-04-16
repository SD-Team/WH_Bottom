import { OrderSizeAccumlate } from './ordersize-accumlate';
export interface OrderSizeByBatch {
    mO_Seq: string;
    purchase_No: string;
    missing_No: string;
    checkInsert: string;
    delivery_No: string;
    material_ID: string; 
    material_Name: string; 
    model_No: string; 
    model_Name: string;
    mO_No: string;
    article: string;
    supplier_ID: string; 
    supplier_Name: string; 
    subcon_No: string; 
    subcon_Name: string; 
    t3_Supplier: string; 
    t3_Supplier_Name: string; 
    purchase_Qty: OrderSizeAccumlate[];
}