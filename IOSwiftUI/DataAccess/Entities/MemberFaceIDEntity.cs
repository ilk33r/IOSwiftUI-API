using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOSwiftUI.DataAccess.Entities;

public class MemberFaceIDEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Required]
    public MemberEntity Member { get; set; }

    [Required]
    [MaxLength(140)]
    public string AuthenticationKey { get; set; }

    [Required]
    public DateTimeOffset PairDate { get; set; }
}
