using System;
using IOSwiftUI.Common.Models.Base;

namespace IOSwiftUI.Common.Models.Discover;

public class DiscoverImageModel: Model
{
    public int MemberId { get; set; }
    public string PublicId { get; set; }
    public string UserName { get; set; }
    public string UserNameAndSurname { get; set; }
    public string UserProfilePicturePublicId { get; set; }
    public DateTimeOffset CreateDate { get; set; }
}
