import BaseRequestModel from "../../../common/models/BaseRequestModel";

class BOImagesDeleteRequestModel extends BaseRequestModel {

    id: number;

    constructor() {
        super();
        
        this.id = 0;
    }
}

export default BOImagesDeleteRequestModel;
