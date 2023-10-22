using IOSwiftUI.Common;
using IOSwiftUI.Core;

namespace IOSwiftUI.BackOffice;

public class BackOfficeMembersViewModel : BackOfficeViewModel
{
    public BOMemberListResponseModel GetMembers(int start, int count)
    {
        int memberCount = DatabaseContext.Members.Count();
        IList<BOMemberModel> paginatedMembers = DatabaseContext.Members
                                                    .Select(m => new BOMemberModel()
                                                    {
                                                        ID = m.ID,
                                                        UserName = m.UserName,
                                                        // Password = m.Password,
                                                        // UserToken = m.UserToken,
                                                        // TokenDate = m.TokenDate,
                                                        RegisterDate = m.RegisterDate,
                                                        BirthDate = m.BirthDate,
                                                        Email = m.Email,
                                                        Name = m.Name,
                                                        Surname = m.Surname,
                                                        LocationName = m.LocationName,
                                                        // LocationLatitude = m.LocationLatitude,
                                                        // LocationLongitude = m.LocationLongitude,
                                                        // ProfilePictureFileName = m.ProfilePictureFileName,
                                                        PhoneNumber = m.PhoneNumber,
                                                        UserStatus = m.UserStatus,
                                                        // DeviceId = m.DeviceId,
                                                        DeviceManifacturer = m.DeviceManifacturer,
                                                        DeviceModel = m.DeviceModel,
                                                        MRZFullString = m.MRZFullString
                                                    })
                                                    .OrderBy(m => m.ID)
                                                    .Skip(start)
                                                    .Take(count)
                                                    .ToList();

        return new BOMemberListResponseModel(memberCount, paginatedMembers);
    }
}
