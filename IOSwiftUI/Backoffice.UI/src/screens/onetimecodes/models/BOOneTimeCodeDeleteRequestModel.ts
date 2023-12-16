import BaseRequestModel from "../../../common/models/BaseRequestModel";

class BOOneTimeCodeDeleteRequestModel extends BaseRequestModel {

    id: number;

    constructor() {
        super();
        
        this.id = 0;
    }
}

export default BOOneTimeCodeDeleteRequestModel;
