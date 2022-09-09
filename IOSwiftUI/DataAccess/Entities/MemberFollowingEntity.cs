using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOSwiftUI.DataAccess.Entities;

public class MemberFollowingEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    public DateTimeOffset FollowDate { get; set; }

    public MemberEntity Member { get; set; }
}
