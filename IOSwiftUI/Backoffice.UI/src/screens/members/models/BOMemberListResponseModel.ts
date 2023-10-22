import BaseResponseModel from "../../../common/models/BaseResponseModel";
import BOMemberModel from "./BOMemberModel";

class BOMemberListResponseModel extends BaseResponseModel {

    count: number;
    members: BOMemberModel[];

    constructor() {
        super();

        this.count = 0;
        this.members = [];
    }
}

export default BOMemberListResponseModel;
