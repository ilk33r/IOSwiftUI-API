import React from "react";
import BreadcrumbNavigationModel from "../../shared/models/BreadcrumbNavigationModel";
import Controller from "../../../presentation/controllers/Controller";
import ListDataItemModel from "../../shared/models/ListDataItemModel";
import ListDataPaginationModel from "../../shared/models/ListDataPaginationModel";
import ListView from "../../shared/views/ListView";
import MemberImagesListState from "../props/MemberImagesListState";
import MemberImagesListProps from "../props/MemberImagesListProps";
import BOImagesListRequestModel from "../models/BOImagesListRequestModel";
import BOImagesListResponseModel from "../models/BOImagesListResponseModel";
import BOImagesDeleteRequestModel from "../models/BOImagesDeleteRequestModel";
import BOImagesUpdateRequestModel from "../models/BOImagesUpdateRequestModel";
import Currencies from "../enumerations/Currencies";


class MemberImagesListController extends Controller<MemberImagesListProps, MemberImagesListState> {

    private requestModel: BOImagesListRequestModel;

    constructor(props: MemberImagesListProps) {
        super(props);

        this.requestModel = new BOImagesListRequestModel();
        this.state = new MemberImagesListState();

        this.pageChangeHandler = this.pageChangeHandler.bind(this);
        this.updateDataHandler = this.updateDataHandler.bind(this);
        this.deleteDataHandler = this.deleteDataHandler.bind(this);
    }

    private loadImagess() {
        this.indicatorPresenter.present();

        const requestPath = "BackOfficeImagess/GetImagess";
        const weakSelf = this;

        this.service.post(requestPath, this.requestModel, function (response: BOImagesListResponseModel) {
            if (weakSelf.handleServiceSuccess(response)) {
                const newState = new MemberImagesListState();
                newState.count = response.count;
                newState.imagesList = response.imagesList;

                weakSelf.setState(newState);
            }
        }, function (error: string) {
            weakSelf.handleServiceError("", error);
        });
    }

    public componentDidMount?(): void {
        this.requestModel.start = 0;
        this.requestModel.count = 25;
        this.loadImagess();
    }

    private pageChangeHandler(start: number, length: number) {
        this.requestModel.start = start;
        this.requestModel.count = length;
        this.loadImagess();
    }

    private updateDataHandler(index: number) {
        const currentImages = this.state.imagesList[index];
        const updateRequestModel = new BOImagesUpdateRequestModel();
        updateRequestModel.id = currentImages.id;
        updateRequestModel.fileName = currentImages.fileName;
        updateRequestModel.createDate = currentImages.createDate;
        updateRequestModel.width = currentImages.width;
        updateRequestModel.height = currentImages.height;
        updateRequestModel.price = currentImages.price;
        updateRequestModel.priceCurrency = currentImages.priceCurrency;
        updateRequestModel.saleAmount = currentImages.saleAmount;

        this.appContext.setObjectForKey("imagesUpdateRequest", updateRequestModel);
        this.navigateToPage("memberImagesUpdate");
    }

    private deleteDataHandler(index: number) {
        const currentImages = this.state.imagesList[index];
        const deleteRequestModel = new BOImagesDeleteRequestModel();
        deleteRequestModel.id = currentImages.id ?? 0;
        
        this.appContext.setObjectForKey("imagesDeleteRequest", deleteRequestModel);
        this.navigateToPage("mamberImagesDelete");
    }

    render() {
        const navigation: BreadcrumbNavigationModel[] = [
            BreadcrumbNavigationModel.initialize("memberImagesList", "Images")
        ];

        const listDataHeaders = [
            'ID',
            'Image',
            'CreateDate',
            'Width',
            'Height',
            'Is Draft',
            'Price',
            'PriceCurrency',
            'SaleAmount',

        ];

        const items = this.state.imagesList.map(images => {
            const itemModel = new ListDataItemModel();
            const id = (images.id == null) ? "-" : images.id.toString();
            const fileName = (images.fileName == null) ? "-" : images.fileName;
            const imageUrl = `${process.env.REACT_APP_API_URL}/ImageAsset/Get?publicId=${encodeURIComponent(fileName)}`;
            const imageHtml = `<img src="${imageUrl}" width="150" />`
            const createDate = (images.createDate == null) ? "-" : images.createDate;
            const width = (images.width == null) ? "-" : images.width.toString();
            const height = (images.height == null) ? "-" : images.height.toString();
            const isDraft = (images.isDraft) ? "true" : "false";
            const price = (images.price == null) ? "-" : images.price.toString();
            const priceCurrency = Currencies.getCurrenciesName(images.priceCurrency);
            const saleAmount = (images.saleAmount == null) ? "-" : images.saleAmount.toString();

            itemModel.itemList = [
                id,
                imageHtml,
                createDate,
                width,
                height,
                isDraft,
                price,
                priceCurrency,
                saleAmount,

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

export default MemberImagesListController;
