using IOBootstrap.NET.MW.DataAccess.Context;
using IOSwiftUI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace IOSwiftUI.DataAccess.Context;

public class DatabaseContext : IODatabaseContext<DatabaseContext>
{
    public virtual DbSet<MemberEntity> Members { get; set; }

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
    }
}
