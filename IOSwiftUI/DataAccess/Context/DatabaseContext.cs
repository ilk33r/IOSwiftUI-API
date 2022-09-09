using IOBootstrap.NET.MW.DataAccess.Context;
using IOSwiftUI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace IOSwiftUI.DataAccess.Context;

public class DatabaseContext : IODatabaseContext<DatabaseContext>
{
    public DbSet<MemberEntity> Members { get; set; }
    public DbSet<MemberFollowingEntity> MemberFollowings { get; set; }
    public DbSet<ImagesEntity> MemberImages { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MemberEntity>().HasIndex(
                entity => new { entity.UserName }).IsUnique(true);
        modelBuilder.Entity<MemberEntity>().HasIndex(
                entity => new { entity.Email }).IsUnique(true);
        modelBuilder.Entity<MemberEntity>().HasIndex(
                entity => new { entity.Name, entity.Surname });

        modelBuilder.Entity<ImagesEntity>().HasIndex(
                entity => new { entity.CreateDate });

        modelBuilder.Entity<MemberFollowingEntity>().HasIndex(
                entity => new { entity.FollowDate });
    }
}
