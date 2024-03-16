using System;
using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common;

public class BOImagesDeleteRequestModel : RequestModel
{

    [Required]
    public int ID { get; set; }

    public BOImagesDeleteRequestModel() : base()
    {
    }
}
