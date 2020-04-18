export interface TransactionMain {
    iD: number;
    transac_Type: string;
    transac_No: string;
    transac_Sheet_No: string;
    can_Move: string
    transac_Time: Date;
    qrCode_ID: string
    qrCode_Version: string;
    mO_No: string;
    Purchase_No: string;
    mO_Seq: string;
    material_ID: string;
    material_Name: string;
    purchase_Qty: number;
    transacted_Qty: number;
    rack_Location: string;
    missing_No: string;
    pickup_No: string;
    updated_Time: Date;
    updated_By: string;
}