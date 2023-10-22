import BaseResponseModel from "../../../common/models/BaseResponseModel";
import BreadcrumbNavigationModel from "../../shared/models/BreadcrumbNavigationModel";
import CalloutTypes from "../../../presentation/constants/CalloutTypes";
import ClientAddRequestModel from "../models/ClientAddRequestModel";
import Controller from "../../../presentation/controllers/Controller";
import FormType from "../../shared/interfaces/FormType";
import FormTypeNumberProps from "../../shared/props/FormTypeNumberProps";
import FormTypeTextProps from "../../shared/props/FormTypeTextProps";
import FormView from "../../shared/views/FormView";
import React from "react";
import ValidationMinAmountRule from "../../../presentation/validations/ValidationMinAmountRule";
import ValidationRequiredRule from "../../../presentation/validations/ValidationRequiredRule";

class ClientsAddController extends Controller<{}, {}> {

    constructor(props: {}) {
        super(props);

        this.handleFormError = this.handleFormError.bind(this);
        this.handleFormSuccess = this.handleFormSuccess.bind(this);
    }

    handleFormError(errorTitle: string, errorMessage: string) {
        this.calloutPresenter.show(CalloutTypes.danger, errorTitle, errorMessage);
    }

    handleFormSuccess(values: string[]) {
        this.indicatorPresenter.present();
        
        const requestPath = `${process.env.REACT_APP_BACKOFFICE_CONTROLLER_NAME}/AddClient`;
        const request = new ClientAddRequestModel();
        request.ClientDescription = values[0];
        request.RequestCount = Number(values[1]);

        const weakSelf = this;
        this.service.post(requestPath, request, function (response: BaseResponseModel) {
            if (weakSelf.handleServiceSuccess(response)) {
                weakSelf.showCalloutAndRedirectToHash("Client has been added successfully.", "clientsList");
            }
        }, function (error: string) {
            weakSelf.handleServiceError("", error);
        });
    }

    render() {
        const navigation: BreadcrumbNavigationModel[] = [
            BreadcrumbNavigationModel.initialize("clientsList", "Clients"),
            BreadcrumbNavigationModel.initialize("clientsAdd", "Add Client")
        ];

        const formElements: FormType[] = [
            FormTypeTextProps.initializeWithValidations("Description", "", true, [ ValidationRequiredRule.initialize("Client name is too sort.", "Invalid client name.") ]),
            FormTypeNumberProps.initializeWithValidations("Request Count", "", true, [ ValidationMinAmountRule.initialize("Request count must be greater than 1.", "Invalid request count.", 1) ])
        ];

        return (
            <React.StrictMode>
                <FormView navigation={navigation} 
                    resourceHome="Home"
                    title="Add a client"
                    submitButtonName="Add"
                    errorHandler={this.handleFormError}
                    successHandler={this.handleFormSuccess}
                    formElements={formElements} />
            </React.StrictMode>
        );
    }
}

export default ClientsAddController;