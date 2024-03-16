import BaseRequestModel from "../../../common/models/BaseRequestModel";
import Currencies from "../enumerations/Currencies";


class BOImagesUpdateRequestModel extends BaseRequestModel {

    id: number | null;
    fileName: string | null;
    createDate: string | null;
    width: number | null;
    height: number | null;
    isDraft: boolean;
    price: number | null;
    priceCurrency: Currencies;
    saleAmount: number | null;

    constructor() {
        super();

        this.id = null;
        this.fileName = null;
        this.createDate = null;
        this.width = null;
        this.height = null;
        this.isDraft = false;
        this.price = null;
        this.priceCurrency = Currencies.USDollar;
        this.saleAmount = null;

    }
}

export default BOImagesUpdateRequestModel;
