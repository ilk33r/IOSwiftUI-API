using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using IOSwiftUI.Common.Enumerations;
using IOSwiftUI.Common.Models.Base;

namespace IOSwiftUI.Common.Models.Members;

public class MemberModel : Model
{
    public int ID { get; set; }

    public string UserName { get; set; }

    [JsonIgnore]
    public string UserToken { get; set; }

    public DateTimeOffset BirthDate { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string LocationName { get; set; }

    public double LocationLatitude { get; set; }

    public double LocationLongitude { get; set; }

    public string ProfilePicturePublicId { get; set; }

    [JsonIgnore]
    public UserStatuses? UserStatus { get; set; }

    [JsonIgnore]
    public ICollection<MemberFollowingModel> Followings { get; set; }
}
