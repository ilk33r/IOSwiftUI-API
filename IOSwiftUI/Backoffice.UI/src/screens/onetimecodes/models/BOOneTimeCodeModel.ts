

class BOOneTimeCodeModel {

    id: number | null;
    phoneNumber: string;
    oneTimeCode: string;
    createDate: string;
    validateDate: string | null;

    constructor() {
        this.id = null;
        this.phoneNumber = "";
        this.oneTimeCode = "";
        this.createDate = "";
        this.validateDate = null;

    }
}

export default BOOneTimeCodeModel;
