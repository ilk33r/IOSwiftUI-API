import BOOneTimeCodeModel from "../models/BOOneTimeCodeModel";

class OneTimeCodeListState {

    count: number;
    oneTimeCodeList: BOOneTimeCodeModel[];

    constructor() {
        this.count = 0;
        this.oneTimeCodeList = [];
    }
}

export default OneTimeCodeListState;
