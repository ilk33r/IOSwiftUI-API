using IOBootstrap.NET.Common.Exceptions.Common;
using IOSwiftUI.Common;
using IOSwiftUI.Core;
using IOSwiftUI.DataAccess.Entities;

namespace IOSwiftUI.BackOffice;

public class BackOfficeMembersViewModel : BackOfficeViewModel
{
    public BOMemberListResponseModel GetMembers(int start, int count)
    {
        int MemberListCount = DatabaseContext.Members.Count();
        IList<BOMemberModel> paginatedMembers = DatabaseContext.Members
                                                    .Select(e => new BOMemberModel()
                                                    {
                                                        ID = e.ID,
                                                        UserName = e.UserName,
                                                        // Password = e.Password,
                                                        // UserToken = e.UserToken,
                                                        // TokenDate = e.TokenDate,
                                                        RegisterDate = e.RegisterDate,
                                                        BirthDate = e.BirthDate,
                                                        Email = e.Email,
                                                        Name = e.Name,
                                                        Surname = e.Surname,
                                                        LocationName = e.LocationName,
                                                        // LocationLatitude = e.LocationLatitude,
                                                        // LocationLongitude = e.LocationLongitude,
                                                        // ProfilePictureFileName = e.ProfilePictureFileName,
                                                        PhoneNumber = e.PhoneNumber,
                                                        UserStatus = e.UserStatus,
                                                        // DeviceId = e.DeviceId,
                                                        DeviceManifacturer = e.DeviceManifacturer,
                                                        DeviceModel = e.DeviceModel,
                                                        MRZFullString = e.MRZFullString,

                                                    })
                                                    .OrderBy(e => e.ID)
                                                    .Skip(start)
                                                    .Take(count)
                                                    .ToList();

        return new BOMemberListResponseModel(MemberListCount, paginatedMembers);
    }

    public void UpdateMember(BOMemberUpdateRequestModel requestModel)
    {
        MemberEntity member = DatabaseContext.Find<MemberEntity>(requestModel.ID) ?? throw new IOInvalidRequestException("Item not found");

        // Update menu item entity
        member.UserName = requestModel.UserName;
        // member.Password = requestModel.Password;
        // member.UserToken = requestModel.UserToken;
        // member.TokenDate = requestModel.TokenDate;
        member.RegisterDate = requestModel.RegisterDate;
        member.BirthDate = requestModel.BirthDate;
        member.Email = requestModel.Email;
        member.Name = requestModel.Name;
        member.Surname = requestModel.Surname;
        member.LocationName = requestModel.LocationName;
        // member.LocationLatitude = requestModel.LocationLatitude;
        // member.LocationLongitude = requestModel.LocationLongitude;
        // member.ProfilePictureFileName = requestModel.ProfilePictureFileName;
        member.PhoneNumber = requestModel.PhoneNumber;
        member.UserStatus = requestModel.UserStatus;
        // member.DeviceId = requestModel.DeviceId;
        member.DeviceManifacturer = requestModel.DeviceManifacturer;
        member.DeviceModel = requestModel.DeviceModel;
        member.MRZFullString = requestModel.MRZFullString;

        // Add menu entity to database
        DatabaseContext.Update(member);
        DatabaseContext.SaveChanges();
    }
}
