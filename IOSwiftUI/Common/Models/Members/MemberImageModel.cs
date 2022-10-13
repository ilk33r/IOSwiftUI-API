using System;
using IOSwiftUI.Common.Models.Base;

namespace IOSwiftUI.Common.Models.Members;

public class MemberImageModel : Model
{
    public int ImageId { get; set; }
    public int MemberId { get; set; }
    public string PublicId { get; set; }
    public string UserName { get; set; }
    public DateTimeOffset CreateDate { get; set; }
}
