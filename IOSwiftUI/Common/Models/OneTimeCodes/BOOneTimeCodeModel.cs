using System;
using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Enumerations;
using IOSwiftUI.Common.Models.Base;

namespace IOSwiftUI.Common;

public class BOOneTimeCodeModel : Model
{
    public int ID { get; set; }

    [Required]
    [StringLength(16)]
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(6)]
    public string OneTimeCode { get; set; }

    [Required]
    public DateTimeOffset CreateDate { get; set; }

    public DateTimeOffset? ValidateDate { get; set; }


}
