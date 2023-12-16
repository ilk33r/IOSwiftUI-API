import BaseResponseModel from "../../../common/models/BaseResponseModel";
import BreadcrumbNavigationModel from "../../shared/models/BreadcrumbNavigationModel";
import Controller from "../../../presentation/controllers/Controller";
import BOOneTimeCodeDeleteRequestModel from "../models/BOOneTimeCodeDeleteRequestModel";
import QuestionView from "../../shared/views/QuestionView";
import React from "react";

class OneTimeCodeDeleteController extends Controller<{}, {}> {

    private _deleteRequest: BOOneTimeCodeDeleteRequestModel;

    constructor(props: {}) {
        super(props);

        this._deleteRequest = this.appContext.objectForKey("oneTimeCodeDeleteRequest") as BOOneTimeCodeDeleteRequestModel;
        if (this._deleteRequest == null) {
            this.navigateToPage("oneTimeCodeList");
            return;
        }

        this.handleFormError = this.handleFormError.bind(this);
        this.handleFormSuccess = this.handleFormSuccess.bind(this);
    }

    private handleFormError() {
        this.navigateToPage("oneTimeCodeList");
    }

    private handleFormSuccess() {
        this.indicatorPresenter.present();

        const requestPath = `BackOfficeOneTimeCodes/DeleteOneTimeCode`;
        const request = new BOOneTimeCodeDeleteRequestModel();
        request.id = this._deleteRequest.id;

        const weakSelf = this;
        this.service.post(requestPath, request, function (response: BaseResponseModel) {
            if (weakSelf.handleServiceSuccess(response)) {
                weakSelf.showCalloutAndRedirectToHash("OneTimeCodes has been deleted successfully.", "oneTimeCodeList");
            }
        }, function (error: string) {
            weakSelf.handleServiceError("", error);
        });
    }

    render() {
        if (this._deleteRequest == null) {
            return (<React.StrictMode></React.StrictMode>);
        }
    
        const navigation: BreadcrumbNavigationModel[] = [
            BreadcrumbNavigationModel.initialize("oneTimeCodeList", "OneTimeCodes"),
            BreadcrumbNavigationModel.initialize("__deleteEntityName__", "Delete OneTimeCodes")
        ];

        return (
            <React.StrictMode>
                <QuestionView navigation={navigation} 
                    resourceHome="Home"
                    title="Delete a onetimecode"
                    questionMessage="Are you sure want to delete this onetimecode ?"
                    errorHandler={this.handleFormError}
                    successHandler={this.handleFormSuccess}
                     />
            </React.StrictMode>
        );
    }
}

export default OneTimeCodeDeleteController;
