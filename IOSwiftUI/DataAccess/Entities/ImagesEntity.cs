using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IOSwiftUI.Common;

namespace IOSwiftUI.DataAccess.Entities;

public class ImagesEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    
    public string FileName { get; set; }

    public DateTimeOffset CreateDate { get; set; }

    public MemberEntity Member { get; set; }

    [DefaultValue(false)]
    public bool IsDraft { get; set; }

    [DefaultValue(false)]
    public bool DirectSale { get; set; }

    [DefaultValue(0)]
    public int Width { get; set; }

    [DefaultValue(0)]
    public int Height { get; set; }

    [DefaultValue(0)]
    public double Price { get; set; }

    [DefaultValue(0)]
    public Currencies PriceCurrency { get; set; }

    [DefaultValue(0)]
    public int SaleAmount { get; set; }
}
