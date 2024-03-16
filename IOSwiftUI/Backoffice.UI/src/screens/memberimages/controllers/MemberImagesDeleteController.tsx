import BaseResponseModel from "../../../common/models/BaseResponseModel";
import BreadcrumbNavigationModel from "../../shared/models/BreadcrumbNavigationModel";
import Controller from "../../../presentation/controllers/Controller";
import BOImagesDeleteRequestModel from "../models/BOImagesDeleteRequestModel";
import QuestionView from "../../shared/views/QuestionView";
import React from "react";

class MemberImagesDeleteController extends Controller<{}, {}> {

    private _deleteRequest: BOImagesDeleteRequestModel;

    constructor(props: {}) {
        super(props);

        this._deleteRequest = this.appContext.objectForKey("imagesDeleteRequest") as BOImagesDeleteRequestModel;
        if (this._deleteRequest == null) {
            this.navigateToPage("imagesList");
            return;
        }

        this.handleFormError = this.handleFormError.bind(this);
        this.handleFormSuccess = this.handleFormSuccess.bind(this);
    }

    private handleFormError() {
        this.navigateToPage("imagesList");
    }

    private handleFormSuccess() {
        this.indicatorPresenter.present();

        const requestPath = `BackOfficeImagess/DeleteImages`;
        const request = new BOImagesDeleteRequestModel();
        request.id = this._deleteRequest.id;

        const weakSelf = this;
        this.service.post(requestPath, request, function (response: BaseResponseModel) {
            if (weakSelf.handleServiceSuccess(response)) {
                weakSelf.showCalloutAndRedirectToHash("Imagess has been deleted successfully.", "imagesList");
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
            BreadcrumbNavigationModel.initialize("imagesList", "Imagess"),
            BreadcrumbNavigationModel.initialize("__deleteEntityName__", "Delete Imagess")
        ];

        return (
            <React.StrictMode>
                <QuestionView navigation={navigation} 
                    resourceHome="Home"
                    title="Delete a images"
                    questionMessage="Are you sure want to delete this images ?"
                    errorHandler={this.handleFormError}
                    successHandler={this.handleFormSuccess}
                     />
            </React.StrictMode>
        );
    }
}

export default MemberImagesDeleteController;
