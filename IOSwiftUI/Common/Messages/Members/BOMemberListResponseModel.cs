using System;
using System.Collections.Generic;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common;

public class BOMemberListResponseModel : ResponseModel
{
    public int Count { get; set; }
    public IList<BOMemberModel> MemberList { get; set; }

    public BOMemberListResponseModel(int count, IList<BOMemberModel> memberList) : base()
    {
        Count = count;
        MemberList = memberList;
    }
}
