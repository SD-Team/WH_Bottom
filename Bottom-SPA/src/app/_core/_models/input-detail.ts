import { DetailSize } from './detail-size';

export class InputDetail {
    input_No: string;
    rack_Location: string;
    qrCode_Id: string;
    suplier_No: string;
    suplier_Name: number;
    plan_No: string;
    batch: string;
    accumated_Qty: number;
    trans_In_Qty: number;
    inStock_Qty: number;
    mat_Id: string;
    mat_Name: string;
    is_Scanned: string;
    detail_Size: DetailSize[];
}
