using IOBootstrap.NET.DataAccess.Context;
using IOSwiftUI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace IOSwiftUI.DataAccess.Context;

public class DatabaseContext : IODatabaseContext<DatabaseContext>
{
    public DbSet<MemberEntity> Members { get; set; }
    public DbSet<MemberFaceIDEntity> MemberFaceIDs { get; set; }
    public DbSet<MemberFollowingEntity> MemberFollowings { get; set; }
    public DbSet<ImagesEntity> MemberImages { get; set; }
    public DbSet<InboxEntity> Inbox { get; set; }
    public DbSet<MessageEntity> DirectMessages { get; set; }
    public DbSet<OneTimeCodeEntity> OneTimeCodes { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        BuildMemberEntity(modelBuilder);
        BuildMemberFaceIDsEntity(modelBuilder);
        BuildMemberImagesEntity(modelBuilder);
        BuildMemberFollowingEntity(modelBuilder);
        BuildMemberInboxEntity(modelBuilder);
        BuildOneTimeCodeEntity(modelBuilder);
    }

    private void BuildMemberEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MemberEntity>().HasIndex(
                entity => new { entity.UserName }).IsUnique(true);
        modelBuilder.Entity<MemberEntity>().HasIndex(
                entity => new { entity.Email }).IsUnique(true);
        modelBuilder.Entity<MemberEntity>().HasIndex(
                entity => new { entity.Name, entity.Surname });

        modelBuilder.Entity<MemberEntity>().Property(memberEntity => memberEntity.LocationLatitude)
                                                .HasPrecision(11, 8);
        modelBuilder.Entity<MemberEntity>().Property(memberEntity => memberEntity.LocationLongitude)
                                                .HasPrecision(11, 8);

        modelBuilder.Entity<MemberEntity>().HasIndex(
                entity => new { entity.PhoneNumber });
    }

    private void BuildMemberFaceIDsEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MemberFaceIDEntity>().HasIndex(
                entity => new { entity.PairDate });
    }

    private void BuildMemberImagesEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImagesEntity>().HasIndex(
                entity => new { entity.CreateDate });

        modelBuilder.Entity<ImagesEntity>().HasIndex(
                entity => new { entity.IsDraft });
    }

    private void BuildMemberFollowingEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MemberFollowingEntity>().HasIndex(
                entity => new { entity.FollowDate });
    }

    private void BuildMemberInboxEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InboxEntity>().HasIndex(
                entity => new { entity.UpdateDate });

        modelBuilder.Entity<MessageEntity>().HasIndex(
                entity => new { entity.MessageDate });
    }

    private void BuildOneTimeCodeEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OneTimeCodeEntity>().HasIndex(
                entity => new { entity.PhoneNumber }).IsUnique(true);
    }
}
