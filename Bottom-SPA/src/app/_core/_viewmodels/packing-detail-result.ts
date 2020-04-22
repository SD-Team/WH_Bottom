import { PackingListDetailModel } from './packing-list-detail-model';
import { SuggestedLocation } from './Suggested-location';
export interface PackingDetailResult {
    packingListDetailModel: PackingListDetailModel[];
    totalQty: number;
    suggestedReturn: SuggestedLocation[];
}