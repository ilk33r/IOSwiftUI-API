import BaseResponseModel from "../../../common/models/BaseResponseModel";
import BreadcrumbNavigationModel from "../../shared/models/BreadcrumbNavigationModel";
import Controller from "../../../presentation/controllers/Controller";
import BOMemberDeleteRequestModel from "../models/BOMemberDeleteRequestModel";
import QuestionView from "../../shared/views/QuestionView";
import React from "react";

class MemberDeleteController extends Controller<{}, {}> {

    private _deleteRequest: BOMemberDeleteRequestModel;

    constructor(props: {}) {
        super(props);

        this._deleteRequest = this.appContext.objectForKey("memberDeleteRequest") as BOMemberDeleteRequestModel;
        if (this._deleteRequest == null) {
            this.navigateToPage("memberList");
            return;
        }

        this.handleFormError = this.handleFormError.bind(this);
        this.handleFormSuccess = this.handleFormSuccess.bind(this);
    }

    private handleFormError() {
        this.navigateToPage("memberList");
    }

    private handleFormSuccess() {
        this.indicatorPresenter.present();

        const requestPath = `BackOfficeMembers/DeleteMember`;
        const request = new BOMemberDeleteRequestModel();
        request.id = this._deleteRequest.id;

        const weakSelf = this;
        this.service.post(requestPath, request, function (response: BaseResponseModel) {
            if (weakSelf.handleServiceSuccess(response)) {
                weakSelf.showCalloutAndRedirectToHash("Members has been deleted successfully.", "memberList");
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
            BreadcrumbNavigationModel.initialize("memberList", "Members"),
            BreadcrumbNavigationModel.initialize("__deleteEntityName__", "Delete Members")
        ];

        return (
            <React.StrictMode>
                <QuestionView navigation={navigation} 
                    resourceHome="Home"
                    title="Delete a member"
                    questionMessage="Are you sure want to delete this member ?"
                    errorHandler={this.handleFormError}
                    successHandler={this.handleFormSuccess}
                     />
            </React.StrictMode>
        );
    }
}

export default MemberDeleteController;
