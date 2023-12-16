import BaseRequestModel from "../../../common/models/BaseRequestModel";

class BOOneTimeCodeListRequestModel extends BaseRequestModel {

    count: number;
    start: number;

    constructor() {
        super();

        this.count = 0;
        this.start = 0;
    }
}

export default BOOneTimeCodeListRequestModel;
