using System;
using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common;

public class BOOneTimeCodeListRequestModel : RequestModel
{
    [Required]
    public int Count { get; set; }

    [Required]
    public int Start { get; set; }
}
