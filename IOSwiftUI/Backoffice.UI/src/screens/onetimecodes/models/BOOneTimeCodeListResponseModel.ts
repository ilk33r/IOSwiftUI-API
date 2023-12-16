import BaseResponseModel from "../../../common/models/BaseResponseModel";
import BOOneTimeCodeModel from "./BOOneTimeCodeModel";

class BOOneTimeCodeListResponseModel extends BaseResponseModel {

    count: number;
    oneTimeCodeList: BOOneTimeCodeModel[];

    constructor() {
        super();

        this.count = 0;
        this.oneTimeCodeList = [];
    }
}

export default BOOneTimeCodeListResponseModel;
