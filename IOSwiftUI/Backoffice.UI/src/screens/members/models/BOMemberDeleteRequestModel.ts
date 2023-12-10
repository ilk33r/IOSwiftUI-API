import BaseRequestModel from "../../../common/models/BaseRequestModel";

class BOMemberDeleteRequestModel extends BaseRequestModel {

    id: number;

    constructor() {
        super();
        
        this.id = 0;
    }
}

export default BOMemberDeleteRequestModel;
