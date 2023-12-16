import BaseRequestModel from "../../../common/models/BaseRequestModel";


class BOOneTimeCodeUpdateRequestModel extends BaseRequestModel {

    id: number | null;
    phoneNumber: string;
    oneTimeCode: string;
    createDate: string;
    validateDate: string | null;

    constructor() {
        super();

        this.id = null;
        this.phoneNumber = "";
        this.oneTimeCode = "";
        this.createDate = "";
        this.validateDate = null;

    }
}

export default BOOneTimeCodeUpdateRequestModel;
