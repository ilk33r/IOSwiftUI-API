import BaseResponseModel from "../../../common/models/BaseResponseModel";
import BOMemberModel from "./BOMemberModel";

class BOMemberListResponseModel extends BaseResponseModel {

    count: number;
    memberList: BOMemberModel[];

    constructor() {
        super();

        this.count = 0;
        this.memberList = [];
    }
}

export default BOMemberListResponseModel;
