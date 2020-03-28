import { QRCodeMainModel } from './qrcode-main-model';
import { PackingDetailResult } from './packing-detail-result';

export interface PackingPrintAll {
    object1: PackingDetailResult;
    qrCodeMainItem: QRCodeMainModel;
}