using System;
using System.Text.Json.Serialization;
using IOSwiftUI.Common.Models.Base;

namespace IOSwiftUI.Common.Models.Members;

public class MemberFriendModel : Model
{
    public int ID { get; set; }

    [JsonIgnore]
    public int OtherMemberID { get; set; }

    public string UserName { get; set; }

    [JsonIgnore]
    public DateTimeOffset FollowDate { get; set; }

    public string UserNameAndSurname { get; set; }

    public string LocationName { get; set; }

    public double LocationLatitude { get; set; }

    public double LocationLongitude { get; set; }

    public string ProfilePicturePublicId { get; set; }
}
