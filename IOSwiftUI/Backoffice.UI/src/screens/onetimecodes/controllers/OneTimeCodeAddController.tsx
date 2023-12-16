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


class OneTimeCodeAddController extends Controller<{}, {}> {

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

        const requestPath = `BackOfficeOneTimeCodes/CreateOneTimeCode`;
        const request = new BOOneTimeCodeUpdateRequestModel();
        request.id = 0;
        request.phoneNumber = values[0];
        request.oneTimeCode = values[1];
        request.createDate = values[2];
        request.validateDate = values[3];

        const weakSelf = this;
        this.service.post(requestPath, request, function (response: BaseResponseModel) {
            if (weakSelf.handleServiceSuccess(response)) {
                weakSelf.showCalloutAndRedirectToHash("OneTimeCode has been added successfully.", "oneTimeCodeList");
            }
        }, function (error: string) {
            weakSelf.handleServiceError("", error);
        });
    }

    render() {
        const navigation: BreadcrumbNavigationModel[] = [
            BreadcrumbNavigationModel.initialize("oneTimeCodeList", "OneTimeCodes"),
            BreadcrumbNavigationModel.initialize("oneTimeCodeAdd", "Create OneTimeCode")
        ];

        const formElements: FormType[] = [
            FormTypeTextProps.initializeWithValidations("PhoneNumber", "", true, [ ValidationRequiredRule.initialize("PhoneNumber is required.", "Invalid PhoneNumber.") ]),
            FormTypeTextProps.initializeWithValidations("OneTimeCode", "", true, [ ValidationRequiredRule.initialize("OneTimeCode is required.", "Invalid OneTimeCode.") ]),
            FormTypeDateProps.initializeWithValidations("CreateDate", "", true, [ ValidationRequiredRule.initialize("CreateDate is required.", "Invalid CreateDate.") ]),
            FormTypeDateProps.initialize("ValidateDate", "", true),

        ];

        return (
            <React.StrictMode>
                <FormView navigation={navigation} 
                    resourceHome="Home"
                    title="Create a onetimecode"
                    submitButtonName="Save"
                    errorHandler={this.handleFormError}
                    successHandler={this.handleFormSuccess}
                    formElements={formElements} />
            </React.StrictMode>
        );
    }
}

export default OneTimeCodeAddController;
