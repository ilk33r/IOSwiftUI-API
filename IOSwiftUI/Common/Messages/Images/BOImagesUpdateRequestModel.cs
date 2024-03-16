using System;
using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Enumerations;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common;

public class BOImagesUpdateRequestModel : RequestModel
{

    [Required]
    public int ID { get; set; }

    public string FileName { get; set; }

    public DateTimeOffset CreateDate { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public bool IsDraft { get; set; }

    public double Price { get; set; }

    public Currencies PriceCurrency { get; set; }

    public int SaleAmount { get; set; }


}
