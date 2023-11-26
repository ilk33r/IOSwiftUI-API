import React from "react";
import Controller from "../../../presentation/controllers/Controller";
import CalloutTypes from "../../../presentation/constants/CalloutTypes";
import FormType from "../../shared/interfaces/FormType";
import FormView from "../../shared/views/FormView";
import FormTypeTextProps from "../../shared/props/FormTypeTextProps";
import FormTypeDateProps from "../../shared/props/FormTypeDateProps";
import FormTypeSelectProps from "../../shared/props/FormTypeSelectProps";
import FormDataOptionModel from "../../shared/models/FormDataOptionModel";
import BaseResponseModel from "../../../common/models/BaseResponseModel";
import BreadcrumbNavigationModel from "../../shared/models/BreadcrumbNavigationModel";
import ValidationRequiredRule from "../../../presentation/validations/ValidationRequiredRule";
import BOMemberUpdateRequestModel from "../models/BOMemberUpdateRequestModel";
import UserStatuses from "../enumerations/UserStatuses";

class MemberUpdateController extends Controller<{}, {}> {

    private _updateRequest: BOMemberUpdateRequestModel;

    constructor(props: {}) {
        super(props);

        this._updateRequest = this.appContext.objectForKey("memberUpdateRequest") as BOMemberUpdateRequestModel;
        if (this._updateRequest == null) {
            this.navigateToPage("memberList");
            return;
        }

        this.handleFormError = this.handleFormError.bind(this);
        this.handleFormSuccess = this.handleFormSuccess.bind(this);
    }

    handleFormError(errorTitle: string, errorMessage: string) {
        this.calloutPresenter.show(CalloutTypes.danger, errorTitle, errorMessage);
    }

    handleFormSuccess(values: string[]) {
        this.indicatorPresenter.present();

        const requestPath = `BackOfficeMembers/UpdateMember`;
        const request = new BOMemberUpdateRequestModel();
        request.id = this._updateRequest.id;
        request.userName = values[0];
        // request.password = values[1];
        // request.userToken = values[2];
        // request.tokenDate = values[3];
        request.registerDate = values[1];
        request.birthDate = values[2];
        request.email = values[3];
        request.name = values[4];
        request.surname = values[5];
        request.locationName = values[6];
        // request.locationLatitude = values[7];
        // request.locationLongitude = values[8];
        // request.profilePictureFileName = values[9];
        request.phoneNumber = values[7];
        request.userStatus = Number(values[8]);
        // request.deviceId = values[9];
        request.deviceManifacturer = values[9];
        request.deviceModel = values[10];
        request.mrzFullString = values[11];

        const weakSelf = this;
        this.service.post(requestPath, request, function (response: BaseResponseModel) {
            if (weakSelf.handleServiceSuccess(response)) {
                weakSelf.showCalloutAndRedirectToHash("Member has been updated successfully.", "memberList");
            }
        }, function (error: string) {
            weakSelf.handleServiceError("", error);
        });
    }

    render() {
        if (this._updateRequest == null) {
            return (<React.StrictMode></React.StrictMode>);
        }
    
        const navigation: BreadcrumbNavigationModel[] = [
            BreadcrumbNavigationModel.initialize("memberList", "Members"),
            BreadcrumbNavigationModel.initialize("memberUpdate", "Update Member")
        ];

        let registerDateString = "";
        if (this._updateRequest.registerDate != null) {
            const registerDate = new Date(this._updateRequest.registerDate);
            registerDateString = this.formatDate(registerDate);
        }

        let birthDateString = "";
        if (this._updateRequest.birthDate != null) {
            const birthDate = new Date(this._updateRequest.birthDate);
            birthDateString = this.formatDate(birthDate);
        }

        const formElements: FormType[] = [
            FormTypeTextProps.initializeWithValidations("UserName", this._updateRequest.userName, true, [ ValidationRequiredRule.initialize("UserName is required.", "Invalid UserName.") ]),
            // FormTypeTextProps.initializeWithValidations("Password", this._updateRequest.password, true, [ ValidationRequiredRule.initialize("Password is required.", "Invalid Password.") ]),
            // FormTypeTextProps.initialize("UserToken", this._updateRequest.userToken ?? "", true),
            // FormTypeDateProps.initialize("TokenDate", this._updateRequest.tokenDate ?? "", true),
            FormTypeDateProps.initializeWithValidations("RegisterDate", registerDateString, true, [ ValidationRequiredRule.initialize("RegisterDate is required.", "Invalid RegisterDate.") ]),
            FormTypeDateProps.initialize("BirthDate", birthDateString, true),
            FormTypeTextProps.initializeWithValidations("Email", this._updateRequest.email, true, [ ValidationRequiredRule.initialize("Email is required.", "Invalid Email.") ]),
            FormTypeTextProps.initializeWithValidations("Name", this._updateRequest.name, true, [ ValidationRequiredRule.initialize("Name is required.", "Invalid Name.") ]),
            FormTypeTextProps.initializeWithValidations("Surname", this._updateRequest.surname, true, [ ValidationRequiredRule.initialize("Surname is required.", "Invalid Surname.") ]),
            FormTypeTextProps.initialize("LocationName", this._updateRequest.locationName ?? "", true),
            // FormTypeTextProps.initialize("LocationLatitude", (this._updateRequest.locationLatitude ?? 0).toString(), true),
            // FormTypeTextProps.initialize("LocationLongitude", (this._updateRequest.locationLongitude ?? 0).toString(), true),
            // FormTypeTextProps.initialize("ProfilePictureFileName", this._updateRequest.profilePictureFileName ?? "", true),
            FormTypeTextProps.initialize("PhoneNumber", this._updateRequest.phoneNumber ?? "", true),
            FormTypeSelectProps.initialize("UserStatus", this._updateRequest.userStatus.toString(), true, [
                FormDataOptionModel.initialize(UserStatuses.getUserStatusesName(UserStatuses.Active), UserStatuses.Active.toString()),
                FormDataOptionModel.initialize(UserStatuses.getUserStatusesName(UserStatuses.Deactivated), UserStatuses.Deactivated.toString()),
                FormDataOptionModel.initialize(UserStatuses.getUserStatusesName(UserStatuses.TemporaryDisabled), UserStatuses.TemporaryDisabled.toString()),
                FormDataOptionModel.initialize(UserStatuses.getUserStatusesName(UserStatuses.Banned), UserStatuses.Banned.toString()),
            ]),
            // FormTypeTextProps.initializeWithValidations("DeviceId", this._updateRequest.deviceId, true, [ ValidationRequiredRule.initialize("DeviceId is required.", "Invalid DeviceId.") ]),
            FormTypeTextProps.initializeWithValidations("DeviceManifacturer", this._updateRequest.deviceManifacturer, true, [ ValidationRequiredRule.initialize("DeviceManifacturer is required.", "Invalid DeviceManifacturer.") ]),
            FormTypeTextProps.initializeWithValidations("DeviceModel", this._updateRequest.deviceModel, true, [ ValidationRequiredRule.initialize("DeviceModel is required.", "Invalid DeviceModel.") ]),
            FormTypeTextProps.initialize("MRZFullString", this._updateRequest.mrzFullString ?? "", true),
        ];

        return (
            <React.StrictMode>
                <FormView navigation={navigation} 
                    resourceHome="Home"
                    title="Update a member"
                    submitButtonName="Save"
                    errorHandler={this.handleFormError}
                    successHandler={this.handleFormSuccess}
                    formElements={formElements} />
            </React.StrictMode>
        );
    }

    private formatDate(date: Date): string {
        const dateMonthValue = date.getMonth() + 1;
        const dateMonth = (dateMonthValue < 10) ? '0' + dateMonthValue.toString() : dateMonthValue.toString();

        const dateDayValue = date.getDate();
        const dateDay = (dateDayValue < 10) ? '0' + dateDayValue.toString() : dateDayValue.toString();

        return date.getFullYear() + '-' + dateMonth + '-' + dateDay;        
    }
}

export default MemberUpdateController;
