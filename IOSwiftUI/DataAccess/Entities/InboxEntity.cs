using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOSwiftUI.DataAccess.Entities;

public class InboxEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    public MemberEntity FromMember { get; set; }

    public MemberEntity ToMember { get; set; }

    [Required]
    public DateTimeOffset CreateDate { get; set; }

    [Required]
    public DateTimeOffset UpdateDate { get; set; }

    [Required]
    public int UnreadMessageCount { get; set; }

    [ForeignKey("InboxID")]
    public ICollection<MessageEntity> Messages { get; set; }

    public MessageEntity LastMessage { get; set; }
}
