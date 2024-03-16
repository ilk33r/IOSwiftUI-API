import BOImagesModel from "../models/BOImagesModel";

class MemberImagesListState {

    count: number;
    imagesList: BOImagesModel[];

    constructor() {
        this.count = 0;
        this.imagesList = [];
    }
}

export default MemberImagesListState;
