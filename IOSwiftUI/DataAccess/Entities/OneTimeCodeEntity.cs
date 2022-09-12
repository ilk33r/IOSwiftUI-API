using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOSwiftUI.DataAccess.Entities;

public class OneTimeCodeEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Required]
    [StringLength(16)]
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(6)]
    public string OneTimeCode { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTimeOffset CreateDate { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTimeOffset ValidateDate { get; set; }

    public bool IsValidated { get; set; }
}
