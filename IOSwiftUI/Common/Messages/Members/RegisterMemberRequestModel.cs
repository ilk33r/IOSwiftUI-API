using System;
using System.ComponentModel.DataAnnotations;
using IOSwiftUI.Common.Messages.Base;

namespace IOSwiftUI.Common.Messages.Members;

public class RegisterMemberRequestModel : RequestModel
{
    [Required]
    [StringLength(128)]
    public string UserName { get; set; }

    [Required]
    [StringLength(512, MinimumLength = 8)]
    public string Password { get; set; }

    public DateTimeOffset BirthDate { get; set; }

    [Required]
    [StringLength(255)]
    public string Email { get; set; }

    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    [Required]
    [StringLength(128)]
    public string Surname { get; set; }

    [StringLength(128)]
    public string LocationName { get; set; }

    public double LocationLatitude { get; set; }

    public double LocationLongitude { get; set; }
}
