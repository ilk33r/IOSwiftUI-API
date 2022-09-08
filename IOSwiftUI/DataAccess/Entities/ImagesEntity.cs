using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOSwiftUI.DataAccess.Entities;

public class ImagesEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    
    public string FileName { get; set; }

    public DateTimeOffset CreateDate { get; set; }

    public MemberEntity Member { get; set; }
}
