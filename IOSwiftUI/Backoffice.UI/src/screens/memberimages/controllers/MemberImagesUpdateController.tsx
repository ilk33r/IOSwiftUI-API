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
import BOImagesUpdateRequestModel from "../models/BOImagesUpdateRequestModel";
import Currencies from "../enumerations/Currencies";


class MemberImagesUpdateController extends Controller<{}, {}> {

    private _updateRequest: BOImagesUpdateRequestModel;

    constructor(props: {}) {
        super(props);

        this._updateRequest = this.appContext.objectForKey("imagesUpdateRequest") as BOImagesUpdateRequestModel;
        if (this._updateRequest == null) {
            this.navigateToPage("imagesList");
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

        const requestPath = `BackOfficeImagess/UpdateImages`;
        const request = new BOImagesUpdateRequestModel();
        request.id = this._updateRequest.id;
        request.fileName = values[0];
        request.createDate = values[1];
        request.width = Number(values[2]);
        request.height = Number(values[3]);
        request.isDraft = (values[4] == "true") ? true : false;
        request.price = Number(values[5]);
        request.priceCurrency = Number(values[6]);
        request.saleAmount = Number(values[7]);

        const weakSelf = this;
        this.service.post(requestPath, request, function (response: BaseResponseModel) {
            if (weakSelf.handleServiceSuccess(response)) {
                weakSelf.showCalloutAndRedirectToHash("Images has been updated successfully.", "memberImagesList");
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
            BreadcrumbNavigationModel.initialize("memberImagesList", "Imagess"),
            BreadcrumbNavigationModel.initialize("memberImagesUpdate", "Update Images")
        ];

        let createDateString = "";
        if (this._updateRequest.createDate != null) {
            const createDate = new Date(this._updateRequest.createDate);
            createDateString = this.formatDate(createDate);
        }


        const formElements: FormType[] = [
            FormTypeTextProps.initialize("FileName", this._updateRequest.fileName ?? "", true),
            FormTypeDateProps.initialize("CreateDate", createDateString, true),
            FormTypeNumberProps.initialize("Width", (this._updateRequest.width ?? 0).toString(), true),
            FormTypeNumberProps.initialize("Height", (this._updateRequest.height ?? 0).toString(), true),
            FormTypeSelectProps.initialize("IsDraft", this._updateRequest.isDraft ? "true" : "false", true, [
                FormDataOptionModel.initialize("true", "true"),
                FormDataOptionModel.initialize("false", "false")
            ]),
            FormTypeTextProps.initialize("Price", (this._updateRequest.price ?? 0).toString(), true),
            FormTypeSelectProps.initialize("PriceCurrency", this._updateRequest.priceCurrency.toString(), true, [
                FormDataOptionModel.initialize(Currencies.getCurrenciesName(Currencies.USDollar), Currencies.USDollar.toString()),
                FormDataOptionModel.initialize(Currencies.getCurrenciesName(Currencies.EURO), Currencies.EURO.toString()),
                FormDataOptionModel.initialize(Currencies.getCurrenciesName(Currencies.TRY), Currencies.TRY.toString()),
            ]),
            FormTypeNumberProps.initialize("SaleAmount", (this._updateRequest.saleAmount ?? 0).toString(), true),

        ];

        return (
            <React.StrictMode>
                <FormView navigation={navigation} 
                    resourceHome="Home"
                    title="Update a images"
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

export default MemberImagesUpdateController;
