using System;
using System.Collections.Generic;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common;

public class BOOneTimeCodeListResponseModel : ResponseModel
{
    public int Count { get; set; }
    public IList<BOOneTimeCodeModel> OneTimeCodeList { get; set; }

    public BOOneTimeCodeListResponseModel(int count, IList<BOOneTimeCodeModel> oneTimeCodeList) : base()
    {
        Count = count;
        OneTimeCodeList = oneTimeCodeList;
    }
}
