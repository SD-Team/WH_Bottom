import { InputDetail } from '../_models/input-detail';

export interface InputSubmitModel {
    transactionList: InputDetail[];
    inputNoList: string[];
}