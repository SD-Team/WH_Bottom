import { PackingListDetailModel } from './packing-list-detail-model';
export interface PackingDetailResult {
    packingListDetailModel1: PackingListDetailModel[];
    packingListDetailModel2: PackingListDetailModel[];
    packingListDetailModel3: PackingListDetailModel[];
    totalRQty: number;
    totalPQty: number;
    totalAct: number;
    suggestedReturn1: string[];
    suggestedReturn2: string[];
}