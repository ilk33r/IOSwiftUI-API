using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IOSwiftUI.Common.Enumerations;

namespace IOSwiftUI.DataAccess.Entities;

public class MemberEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Required]
    [StringLength(128)]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    [StringLength(48)]
    public string UserToken { get; set; }
    
    public DateTimeOffset TokenDate { get; set; }

    [Required]
    public DateTimeOffset RegisterDate { get; set; }

    [Column(TypeName = "DATETIME")]
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

    public string ProfilePictureFileName { get; set; }

    [StringLength(16)]
    public string PhoneNumber { get; set; }

    [StringLength(6)]
    public string OneTimeCode { get; set; }

    [Required]
    public UserStatuses UserStatus { get; set; }

    [ForeignKey("MemberID")]
    public ICollection<ImagesEntity> Images { get; set; }

    [Required]
    [StringLength(128)]
    public string DeviceId { get; set; }
        
    [Required]
    [StringLength(32)]
    public string DeviceManifacturer { get; set; }

    [Required]
    [StringLength(32)]
    public string DeviceModel { get; set; }
}
