import React from "react";
import Controller from "../../../presentation/controllers/Controller";
import CalloutTypes from "../../../presentation/constants/CalloutTypes";
import FormType from "../../shared/interfaces/FormType";
import FormView from "../../shared/views/FormView";
import FormTypeNumberProps from "../../shared/props/FormTypeNumberProps";
import FormTypeTextProps from "../../shared/props/FormTypeTextProps";
import FormTypeDateProps from "../../shared/props/FormTypeDateProps";
import FormTypeSelectProps from "../../shared/props/FormTypeSelectProps";
import FormDataOptionModel from "../../shared/models/FormDataOptionModel";
import BaseResponseModel from "../../../common/models/BaseResponseModel";
import BreadcrumbNavigationModel from "../../shared/models/BreadcrumbNavigationModel";
import ValidationRequiredRule from "../../../presentation/validations/ValidationRequiredRule";
import BOMemberUpdateRequestModel from "../models/BOMemberUpdateRequestModel";
import UserStatuses from "../enumerations/UserStatuses";


class MemberAddController extends Controller<{}, {}> {

    constructor(props: {}) {
        super(props);

        this.handleFormError = this.handleFormError.bind(this);
        this.handleFormSuccess = this.handleFormSuccess.bind(this);
    }

    private handleFormError(errorTitle: string, errorMessage: string) {
        this.calloutPresenter.show(CalloutTypes.danger, errorTitle, errorMessage);
    }

    private handleFormSuccess(values: string[]) {
        this.indicatorPresenter.present();

        const requestPath = `BackOfficeMembers/CreateMember`;
        const request = new BOMemberUpdateRequestModel();
        request.userName = values[0];
        request.password = values[1];
        request.userToken = values[2];
        request.tokenDate = values[3];
        request.registerDate = values[4];
        request.birthDate = values[5];
        request.email = values[6];
        request.name = values[7];
        request.surname = values[8];
        request.locationName = values[9];
        request.locationLatitude = Number(values[10]);
        request.locationLongitude = Number(values[11]);
        request.profilePictureFileName = values[12];
        request.phoneNumber = values[13];
        request.userStatus = Number(values[14]);
        request.deviceId = values[15];
        request.deviceManifacturer = values[16];
        request.deviceModel = values[17];
        request.mrzFullString = values[18];

        const weakSelf = this;
        this.service.post(requestPath, request, function (response: BaseResponseModel) {
            if (weakSelf.handleServiceSuccess(response)) {
                weakSelf.showCalloutAndRedirectToHash("Member has been added successfully.", "memberList");
            }
        }, function (error: string) {
            weakSelf.handleServiceError("", error);
        });
    }

    render() {
        const navigation: BreadcrumbNavigationModel[] = [
            BreadcrumbNavigationModel.initialize("memberList", "Members"),
            BreadcrumbNavigationModel.initialize("memberCreate", "Create Member")
        ];

        const formElements: FormType[] = [
            FormTypeTextProps.initializeWithValidations("UserName", "", true, [ ValidationRequiredRule.initialize("UserName is required.", "Invalid UserName.") ]),
            FormTypeTextProps.initializeWithValidations("Password", "", true, [ ValidationRequiredRule.initialize("Password is required.", "Invalid Password.") ]),
            FormTypeTextProps.initialize("UserToken", "", true),
            FormTypeDateProps.initialize("TokenDate", "", true),
            FormTypeDateProps.initializeWithValidations("RegisterDate", "", true, [ ValidationRequiredRule.initialize("RegisterDate is required.", "Invalid RegisterDate.") ]),
            FormTypeDateProps.initialize("BirthDate", "", true),
            FormTypeTextProps.initializeWithValidations("Email", "", true, [ ValidationRequiredRule.initialize("Email is required.", "Invalid Email.") ]),
            FormTypeTextProps.initializeWithValidations("Name", "", true, [ ValidationRequiredRule.initialize("Name is required.", "Invalid Name.") ]),
            FormTypeTextProps.initializeWithValidations("Surname", "", true, [ ValidationRequiredRule.initialize("Surname is required.", "Invalid Surname.") ]),
            FormTypeTextProps.initialize("LocationName", "", true),
            FormTypeTextProps.initialize("LocationLatitude", "".toString(), true),
            FormTypeTextProps.initialize("LocationLongitude", "".toString(), true),
            FormTypeTextProps.initialize("ProfilePictureFileName", "", true),
            FormTypeTextProps.initialize("PhoneNumber", "", true),
            FormTypeSelectProps.initializeWithValidations("UserStatus", "", true, [
                FormDataOptionModel.initialize(UserStatuses.getUserStatusesName(UserStatuses.Active), UserStatuses.Active.toString()),
                FormDataOptionModel.initialize(UserStatuses.getUserStatusesName(UserStatuses.Deactivated), UserStatuses.Deactivated.toString()),
                FormDataOptionModel.initialize(UserStatuses.getUserStatusesName(UserStatuses.TemporaryDisabled), UserStatuses.TemporaryDisabled.toString()),
                FormDataOptionModel.initialize(UserStatuses.getUserStatusesName(UserStatuses.Banned), UserStatuses.Banned.toString()),
            ], [ ValidationRequiredRule.initialize("UserStatus is required.", "Invalid UserStatus.") ]),
            FormTypeTextProps.initializeWithValidations("DeviceId", "", true, [ ValidationRequiredRule.initialize("DeviceId is required.", "Invalid DeviceId.") ]),
            FormTypeTextProps.initializeWithValidations("DeviceManifacturer", "", true, [ ValidationRequiredRule.initialize("DeviceManifacturer is required.", "Invalid DeviceManifacturer.") ]),
            FormTypeTextProps.initializeWithValidations("DeviceModel", "", true, [ ValidationRequiredRule.initialize("DeviceModel is required.", "Invalid DeviceModel.") ]),
            FormTypeTextProps.initialize("MRZFullString", "", true),

        ];

        return (
            <React.StrictMode>
                <FormView navigation={navigation} 
                    resourceHome="Home"
                    title="Create a member"
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

export default MemberAddController;
