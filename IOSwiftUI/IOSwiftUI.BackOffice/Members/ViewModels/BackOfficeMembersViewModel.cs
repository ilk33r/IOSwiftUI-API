using IOSwiftUI.Common;
using IOSwiftUI.Core;

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
}
