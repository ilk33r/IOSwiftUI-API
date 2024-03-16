import BaseResponseModel from "../../../common/models/BaseResponseModel";
import BOImagesModel from "./BOImagesModel";

class BOImagesListResponseModel extends BaseResponseModel {

    count: number;
    imagesList: BOImagesModel[];

    constructor() {
        super();

        this.count = 0;
        this.imagesList = [];
    }
}

export default BOImagesListResponseModel;
