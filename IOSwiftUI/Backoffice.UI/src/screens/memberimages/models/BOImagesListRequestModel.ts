import BaseRequestModel from "../../../common/models/BaseRequestModel";

class BOImagesListRequestModel extends BaseRequestModel {

    count: number;
    start: number;
    memberID: number;

    constructor() {
        super();

        this.count = 0;
        this.start = 0;
        this.memberID = 0;
    }
}

export default BOImagesListRequestModel;
