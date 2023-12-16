using IOBootstrap.NET.Common.Exceptions.Common;
using IOSwiftUI.Common;
using IOSwiftUI.Core;
using IOSwiftUI.DataAccess.Entities;

namespace IOSwiftUI.BackOffice;

public class BackOfficeOneTimeCodesViewModel : BackOfficeViewModel
{
    public BOOneTimeCodeListResponseModel GetOneTimeCodes(int start, int count)
    {
        int OneTimeCodeListCount = DatabaseContext.OneTimeCodes.Count();
        IList<BOOneTimeCodeModel> paginatedOneTimeCodes = DatabaseContext.OneTimeCodes
                                                    .Select(e => new BOOneTimeCodeModel()
                                                    {
                                                        ID = e.ID,
                                                        PhoneNumber = e.PhoneNumber,
                                                        OneTimeCode = e.OneTimeCode,
                                                        CreateDate = e.CreateDate,
                                                        ValidateDate = e.ValidateDate,

                                                    })
                                                    .OrderBy(e => e.ID)
                                                    .Skip(start)
                                                    .Take(count)
                                                    .ToList();

        return new BOOneTimeCodeListResponseModel(OneTimeCodeListCount, paginatedOneTimeCodes);
    }

    public void UpdateOneTimeCode(BOOneTimeCodeUpdateRequestModel requestModel)
    {
        OneTimeCodeEntity onetimecode = DatabaseContext.Find<OneTimeCodeEntity>(requestModel.ID) ?? throw new IOInvalidRequestException("Item not found");

        // Update item entity
        onetimecode.ID = requestModel.ID;
        onetimecode.PhoneNumber = requestModel.PhoneNumber;
        onetimecode.OneTimeCode = requestModel.OneTimeCode;
        onetimecode.CreateDate = requestModel.CreateDate;
        onetimecode.ValidateDate = requestModel.ValidateDate;

        // Update entity to database
        DatabaseContext.Update(onetimecode);
        DatabaseContext.SaveChanges();
    }

    public void DeleteOneTimeCode(int id)
    {
        // Obtain item entity
        OneTimeCodeEntity? onetimecode = DatabaseContext.Find<OneTimeCodeEntity>(id);

        // Check item is not exists
        if (onetimecode == null)
        {
            return;
        }

        // Remove entity
        DatabaseContext.Remove(onetimecode);
        DatabaseContext.SaveChanges();
    }

    public void CreateOneTimeCode(BOOneTimeCodeAddRequestModel requestModel)
    {
        OneTimeCodeEntity onetimecode = new OneTimeCodeEntity();

        // Create item entity
        onetimecode.PhoneNumber = requestModel.PhoneNumber;
        onetimecode.OneTimeCode = requestModel.OneTimeCode;
        onetimecode.CreateDate = requestModel.CreateDate;
        onetimecode.ValidateDate = requestModel.ValidateDate;


        // Create entity to database
        DatabaseContext.Add(onetimecode);
        DatabaseContext.SaveChanges();
    }
}
