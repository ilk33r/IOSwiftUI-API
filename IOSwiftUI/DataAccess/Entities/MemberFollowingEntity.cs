using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOSwiftUI.DataAccess.Entities;

public class MemberFollowingEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Required]
    public DateTimeOffset FollowDate { get; set; }

    [Required]
    public MemberEntity Member { get; set; }

    [Required]
    public MemberEntity FollowingMember { get; set; }
}
