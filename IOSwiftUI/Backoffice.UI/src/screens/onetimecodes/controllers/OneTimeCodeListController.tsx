import React from "react";
import BreadcrumbNavigationModel from "../../shared/models/BreadcrumbNavigationModel";
import Controller from "../../../presentation/controllers/Controller";
import ListDataItemModel from "../../shared/models/ListDataItemModel";
import ListDataPaginationModel from "../../shared/models/ListDataPaginationModel";
import ListView from "../../shared/views/ListView";
import OneTimeCodeListState from "../props/OneTimeCodeListState";
import OneTimeCodeListProps from "../props/OneTimeCodeListProps";
import BOOneTimeCodeListRequestModel from "../models/BOOneTimeCodeListRequestModel";
import BOOneTimeCodeListResponseModel from "../models/BOOneTimeCodeListResponseModel";
import BOOneTimeCodeDeleteRequestModel from "../models/BOOneTimeCodeDeleteRequestModel";
import BOOneTimeCodeUpdateRequestModel from "../models/BOOneTimeCodeUpdateRequestModel";


class OneTimeCodeListController extends Controller<OneTimeCodeListProps, OneTimeCodeListState> {

    private requestModel: BOOneTimeCodeListRequestModel;

    constructor(props: OneTimeCodeListProps) {
        super(props);

        this.requestModel = new BOOneTimeCodeListRequestModel();
        this.state = new OneTimeCodeListState();

        this.pageChangeHandler = this.pageChangeHandler.bind(this);
        this.updateDataHandler = this.updateDataHandler.bind(this);
        this.deleteDataHandler = this.deleteDataHandler.bind(this);
    }

    private loadOneTimeCodes() {
        this.indicatorPresenter.present();

        const requestPath = "BackOfficeOneTimeCodes/GetOneTimeCodes";
        const weakSelf = this;

        this.service.post(requestPath, this.requestModel, function (response: BOOneTimeCodeListResponseModel) {
            if (weakSelf.handleServiceSuccess(response)) {
                const newState = new OneTimeCodeListState();
                newState.count = response.count;
                newState.oneTimeCodeList = response.oneTimeCodeList;

                weakSelf.setState(newState);
            }
        }, function (error: string) {
            weakSelf.handleServiceError("", error);
        });
    }

    public componentDidMount?(): void {
        this.requestModel.start = 0;
        this.requestModel.count = 25;
        this.loadOneTimeCodes();
    }

    private pageChangeHandler(start: number, length: number) {
        this.requestModel.start = start;
        this.requestModel.count = length;
        this.loadOneTimeCodes();
    }

    private updateDataHandler(index: number) {
        const currentOneTimeCode = this.state.oneTimeCodeList[index];
        const updateRequestModel = new BOOneTimeCodeUpdateRequestModel();
        updateRequestModel.id = currentOneTimeCode.id;
        updateRequestModel.phoneNumber = currentOneTimeCode.phoneNumber;
        updateRequestModel.oneTimeCode = currentOneTimeCode.oneTimeCode;
        updateRequestModel.createDate = currentOneTimeCode.createDate;
        updateRequestModel.validateDate = currentOneTimeCode.validateDate;

        this.appContext.setObjectForKey("oneTimeCodeUpdateRequest", updateRequestModel);
        this.navigateToPage("oneTimeCodeUpdate");
    }

    private deleteDataHandler(index: number) {
        const currentOneTimeCode = this.state.oneTimeCodeList[index];
        const deleteRequestModel = new BOOneTimeCodeDeleteRequestModel();
        deleteRequestModel.id = currentOneTimeCode.id ?? 0;
        
        this.appContext.setObjectForKey("oneTimeCodeDeleteRequest", deleteRequestModel);
        this.navigateToPage("oneTimeCodeDelete");
    }

    render() {
        const navigation: BreadcrumbNavigationModel[] = [
            BreadcrumbNavigationModel.initialize("oneTimeCodeList", "OneTimeCodes")
        ];

        const listDataHeaders = [
            'ID',
            'PhoneNumber',
            'OneTimeCode',
            'CreateDate',
            'ValidateDate',

        ];

        const items = this.state.oneTimeCodeList.map(onetimecode => {
            const itemModel = new ListDataItemModel();
            const id = (onetimecode.id == null) ? "-" : onetimecode.id.toString();
            const phoneNumber = onetimecode.phoneNumber;
            const oneTimeCode = onetimecode.oneTimeCode;
            const createDate = onetimecode.createDate;
            const validateDate = (onetimecode.validateDate == null) ? "-" : onetimecode.validateDate;

            itemModel.itemList = [
                id,
                phoneNumber,
                oneTimeCode,
                createDate,
                validateDate,

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

export default OneTimeCodeListController;
