import BaseRequestModel from "../../../common/models/BaseRequestModel";
import UserStatuses from "../enumerations/UserStatuses";


class BOMemberUpdateRequestModel extends BaseRequestModel {

    id: number | null;
    userName: string;
    password: string;
    userToken: string | null;
    tokenDate: string | null;
    registerDate: string;
    birthDate: string | null;
    email: string;
    name: string;
    surname: string;
    locationName: string | null;
    locationLatitude: number | null;
    locationLongitude: number | null;
    profilePictureFileName: string | null;
    phoneNumber: string | null;
    userStatus: UserStatuses;
    deviceId: string;
    deviceManifacturer: string;
    deviceModel: string;
    mrzFullString: string | null;

    constructor() {
        super();

        this.id = null;
        this.userName = "";
        this.password = "";
        this.userToken = null;
        this.tokenDate = null;
        this.registerDate = "";
        this.birthDate = null;
        this.email = "";
        this.name = "";
        this.surname = "";
        this.locationName = null;
        this.locationLatitude = null;
        this.locationLongitude = null;
        this.profilePictureFileName = null;
        this.phoneNumber = null;
        this.userStatus = UserStatuses.Active;
        this.deviceId = "";
        this.deviceManifacturer = "";
        this.deviceModel = "";
        this.mrzFullString = null;

    }
}

export default BOMemberUpdateRequestModel;
