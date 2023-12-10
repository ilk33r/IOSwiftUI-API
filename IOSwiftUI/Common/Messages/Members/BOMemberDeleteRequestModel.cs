using System;
using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common;

public class BOMemberDeleteRequestModel : RequestModel
{

    [Required]
    public int ID { get; set; }

    public BOMemberDeleteRequestModel() : base()
    {
    }
}
