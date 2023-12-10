import React from "react";
import BreadcrumbNavigationModel from "../../shared/models/BreadcrumbNavigationModel";
import Controller from "../../../presentation/controllers/Controller";
import ListDataItemModel from "../../shared/models/ListDataItemModel";
import ListDataPaginationModel from "../../shared/models/ListDataPaginationModel";
import ListView from "../../shared/views/ListView";
import MemberListState from "../props/MemberListState";
import MemberListProps from "../props/MemberListProps";
import BOMemberListRequestModel from "../models/BOMemberListRequestModel";
import BOMemberListResponseModel from "../models/BOMemberListResponseModel";
import BOMemberDeleteRequestModel from "../models/BOMemberDeleteRequestModel";
import BOMemberUpdateRequestModel from "../models/BOMemberUpdateRequestModel";
import UserStatuses from "../enumerations/UserStatuses";


class MemberListController extends Controller<MemberListProps, MemberListState> {

    private requestModel: BOMemberListRequestModel;

    constructor(props: MemberListProps) {
        super(props);

        this.requestModel = new BOMemberListRequestModel();
        this.state = new MemberListState();

        this.pageChangeHandler = this.pageChangeHandler.bind(this);
        this.updateDataHandler = this.updateDataHandler.bind(this);
        this.deleteDataHandler = this.deleteDataHandler.bind(this);
    }

    private loadMembers() {
        this.indicatorPresenter.present();

        const requestPath = "BackOfficeMembers/GetMembers";
        const weakSelf = this;

        this.service.post(requestPath, this.requestModel, function (response: BOMemberListResponseModel) {
            if (weakSelf.handleServiceSuccess(response)) {
                const newState = new MemberListState();
                newState.count = response.count;
                newState.memberList = response.memberList;

                weakSelf.setState(newState);
            }
        }, function (error: string) {
            weakSelf.handleServiceError("", error);
        });
    }

    public componentDidMount?(): void {
        this.requestModel.start = 0;
        this.requestModel.count = 25;
        this.loadMembers();
    }

    private pageChangeHandler(start: number, length: number) {
        this.requestModel.start = start;
        this.requestModel.count = length;
        this.loadMembers();
    }

    private updateDataHandler(index: number) {
        const currentMember = this.state.memberList[index];
        const updateRequestModel = new BOMemberUpdateRequestModel();
        updateRequestModel.id = currentMember.id;
        updateRequestModel.userName = currentMember.userName;
        // updateRequestModel.password = currentMember.password;
        // updateRequestModel.userToken = currentMember.userToken;
        // updateRequestModel.tokenDate = currentMember.tokenDate;
        updateRequestModel.registerDate = currentMember.registerDate;
        updateRequestModel.birthDate = currentMember.birthDate;
        updateRequestModel.email = currentMember.email;
        updateRequestModel.name = currentMember.name;
        updateRequestModel.surname = currentMember.surname;
        updateRequestModel.locationName = currentMember.locationName;
        // updateRequestModel.locationLatitude = currentMember.locationLatitude;
        // updateRequestModel.locationLongitude = currentMember.locationLongitude;
        // updateRequestModel.profilePictureFileName = currentMember.profilePictureFileName;
        updateRequestModel.phoneNumber = currentMember.phoneNumber;
        updateRequestModel.userStatus = currentMember.userStatus;
        // updateRequestModel.deviceId = currentMember.deviceId;
        updateRequestModel.deviceManifacturer = currentMember.deviceManifacturer;
        updateRequestModel.deviceModel = currentMember.deviceModel;
        updateRequestModel.mrzFullString = currentMember.mrzFullString;

        this.appContext.setObjectForKey("memberUpdateRequest", updateRequestModel);
        this.navigateToPage("memberUpdate");
    }

    private deleteDataHandler(index: number) {
        const currentMember = this.state.memberList[index];
        const deleteRequestModel = new BOMemberDeleteRequestModel();
        deleteRequestModel.id = currentMember.id ?? 0;
        
        this.appContext.setObjectForKey("memberDeleteRequest", deleteRequestModel);
        this.navigateToPage("memberDelete");
    }

    render() {
        const navigation: BreadcrumbNavigationModel[] = [
            BreadcrumbNavigationModel.initialize("memberList", "Members")
        ];

        const listDataHeaders = [
            'ID',
            'UserName',
            // 'Password',
            // 'UserToken',
            // 'TokenDate',
            'RegisterDate',
            'BirthDate',
            'Email',
            'Name',
            'Surname',
            'LocationName',
            // 'LocationLatitude',
            // 'LocationLongitude',
            // 'ProfilePictureFileName',
            'PhoneNumber',
            'UserStatus',
            // 'DeviceId',
            'DeviceManifacturer',
            'DeviceModel',
            'MRZFullString',

        ];

        const items = this.state.memberList.map(member => {
            const itemModel = new ListDataItemModel();
            const id = (member.id == null) ? "-" : member.id.toString();
            const userName = member.userName;
            // const password = member.password;
            // const userToken = (member.userToken == null) ? "-" : member.userToken;
            // const tokenDate = (member.tokenDate == null) ? "-" : member.tokenDate;
            const registerDate = member.registerDate;
            const birthDate = (member.birthDate == null) ? "-" : member.birthDate;
            const email = member.email;
            const name = member.name;
            const surname = member.surname;
            const locationName = (member.locationName == null) ? "-" : member.locationName;
            // const locationLatitude = (member.locationLatitude == null) ? "-" : member.locationLatitude.toString();
            // const locationLongitude = (member.locationLongitude == null) ? "-" : member.locationLongitude.toString();
            // const profilePictureFileName = (member.profilePictureFileName == null) ? "-" : member.profilePictureFileName;
            const phoneNumber = (member.phoneNumber == null) ? "-" : member.phoneNumber;
            const userStatus = UserStatuses.getUserStatusesName(member.userStatus);
            // const deviceId = member.deviceId;
            const deviceManifacturer = member.deviceManifacturer;
            const deviceModel = member.deviceModel;
            const mrzFullString = (member.mrzFullString == null) ? "-" : member.mrzFullString;

            itemModel.itemList = [
                id,
                userName,
                // password,
                // userToken,
                // tokenDate,
                registerDate,
                birthDate,
                email,
                name,
                surname,
                locationName,
                // locationLatitude,
                // locationLongitude,
                // profilePictureFileName,
                phoneNumber,
                userStatus,
                // deviceId,
                deviceManifacturer,
                deviceModel,
                mrzFullString,

            ];

            return itemModel;
        });

        const pagination = new ListDataPaginationModel();
        pagination.start = this.requestModel.start;
        pagination.length = this.requestModel.count;
        pagination.count = this.state.count;
        pagination.pageClickHandler = this.pageChangeHandler;

        return (
            <React.StrictMode>
                <ListView navigation={navigation} 
                    listDataHeaders={listDataHeaders} 
                    items={items}
                    resourceDelete="Delete"
                    resourceEdit="Edit"
                    resourceHome="Home"
                    resourceOptions="Options"
                    resourceSelect=""
                    extras={null}
                    deleteDataHandler={this.deleteDataHandler}
                    updateDataHandler={this.updateDataHandler}
                    selectDataHandler={null}
                    pagination={pagination} />
            </React.StrictMode>
        );
    }
}

export default MemberListController;
