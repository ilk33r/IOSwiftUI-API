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
import BOOneTimeCodeUpdateRequestModel from "../models/BOOneTimeCodeUpdateRequestModel";


class OneTimeCodeUpdateController extends Controller<{}, {}> {

    private _updateRequest: BOOneTimeCodeUpdateRequestModel;

    constructor(props: {}) {
        super(props);

        this._updateRequest = this.appContext.objectForKey("oneTimeCodeUpdateRequest") as BOOneTimeCodeUpdateRequestModel;
        if (this._updateRequest == null) {
            this.navigateToPage("oneTimeCodeList");
            return;
        }

        this.handleFormError = this.handleFormError.bind(this);
        this.handleFormSuccess = this.handleFormSuccess.bind(this);
    }

    private handleFormError(errorTitle: string, errorMessage: string) {
        this.calloutPresenter.show(CalloutTypes.danger, errorTitle, errorMessage);
    }

    private handleFormSuccess(values: string[]) {
        this.indicatorPresenter.present();

        const requestPath = `BackOfficeOneTimeCodes/UpdateOneTimeCode`;
        const request = new BOOneTimeCodeUpdateRequestModel();
        request.id = this._updateRequest.id;
        request.phoneNumber = values[0];
        request.oneTimeCode = values[1];
        request.createDate = values[2];
        request.validateDate = values[3];

        const weakSelf = this;
        this.service.post(requestPath, request, function (response: BaseResponseModel) {
            if (weakSelf.handleServiceSuccess(response)) {
                weakSelf.showCalloutAndRedirectToHash("OneTimeCode has been updated successfully.", "oneTimeCodeList");
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
            BreadcrumbNavigationModel.initialize("oneTimeCodeList", "OneTimeCodes"),
            BreadcrumbNavigationModel.initialize("oneTimeCodeUpdate", "Update OneTimeCode")
        ];

        let createDateString = "";
        if (this._updateRequest.createDate != null) {
            const createDate = new Date(this._updateRequest.createDate);
            createDateString = this.formatDate(createDate);
        }

        let validateDateString = "";
        if (this._updateRequest.validateDate != null) {
            const validateDate = new Date(this._updateRequest.validateDate);
            validateDateString = this.formatDate(validateDate);
        }


        const formElements: FormType[] = [
            FormTypeTextProps.initializeWithValidations("PhoneNumber", this._updateRequest.phoneNumber, true, [ ValidationRequiredRule.initialize("PhoneNumber is required.", "Invalid PhoneNumber.") ]),
            FormTypeTextProps.initializeWithValidations("OneTimeCode", this._updateRequest.oneTimeCode, true, [ ValidationRequiredRule.initialize("OneTimeCode is required.", "Invalid OneTimeCode.") ]),
            FormTypeDateProps.initializeWithValidations("CreateDate", createDateString, true, [ ValidationRequiredRule.initialize("CreateDate is required.", "Invalid CreateDate.") ]),
            FormTypeDateProps.initialize("ValidateDate", validateDateString, true),

        ];

        return (
            <React.StrictMode>
                <FormView navigation={navigation} 
                    resourceHome="Home"
                    title="Update a onetimecode"
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

export default OneTimeCodeUpdateController;
