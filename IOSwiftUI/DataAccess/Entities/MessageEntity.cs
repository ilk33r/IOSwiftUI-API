using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOSwiftUI.DataAccess.Entities;

public class MessageEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    public int InboxID { get; set; }

    public MemberEntity FromMember { get; set; }

    public MemberEntity ToMember { get; set; }

    [Required]
    [StringLength(512)]
    public string Message { get; set; }

    [Required]
    public DateTimeOffset MessageDate { get; set; }
}
