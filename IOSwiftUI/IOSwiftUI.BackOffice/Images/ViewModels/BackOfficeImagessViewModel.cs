using IOBootstrap.NET.Common.Exceptions.Common;
using IOSwiftUI.Common;
using IOSwiftUI.Core;
using IOSwiftUI.DataAccess.Entities;

namespace IOSwiftUI.BackOffice;

public class BackOfficeImagessViewModel : BackOfficeViewModel
{
    public BOImagesListResponseModel GetImagess(int start, int count, int memberID)
    {
        int ImagesListCount = DatabaseContext.MemberImages.Count();
        IList<BOImagesModel> paginatedImagess = DatabaseContext.MemberImages
                                                    .Select(e => new BOImagesModel()
                                                    {
                                                        ID = e.ID,
                                                        FileName = e.FileName,
                                                        CreateDate = e.CreateDate,
                                                        Width = e.Width,
                                                        Height = e.Height,
                                                        IsDraft = e.IsDraft,
                                                        Price = e.Price,
                                                        PriceCurrency = e.PriceCurrency,
                                                        SaleAmount = e.SaleAmount,

                                                    })
                                                    .OrderBy(e => e.ID)
                                                    .Skip(start)
                                                    .Take(count)
                                                    .ToList();

        foreach(BOImagesModel image in paginatedImagess)
        {
            image.FileName = CreatePublicId(image.FileName);
        }

        return new BOImagesListResponseModel(ImagesListCount, paginatedImagess);
    }

    public void UpdateImages(BOImagesUpdateRequestModel requestModel)
    {
        ImagesEntity images = DatabaseContext.Find<ImagesEntity>(requestModel.ID) ?? throw new IOInvalidRequestException("Item not found");

        // Update item entity
        images.ID = requestModel.ID;
        // images.FileName = requestModel.FileName;
        images.CreateDate = requestModel.CreateDate;
        images.Width = requestModel.Width;
        images.Height = requestModel.Height;
        images.IsDraft = requestModel.IsDraft;
        images.Price = requestModel.Price;
        images.PriceCurrency = requestModel.PriceCurrency;
        images.SaleAmount = requestModel.SaleAmount;

        // Update entity to database
        DatabaseContext.Update(images);
        DatabaseContext.SaveChanges();
    }

    public void DeleteImages(int id)
    {
        // Obtain item entity
        ImagesEntity? images = DatabaseContext.Find<ImagesEntity>(id);

        // Check item is not exists
        if (images == null)
        {
            return;
        }

        // Remove entity
        DatabaseContext.Remove(images);
        DatabaseContext.SaveChanges();
    }
}
