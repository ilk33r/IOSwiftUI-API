using IOBootstrap.NET.MW.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace IOSwiftUI.DataAccess.Context;

public class DatabaseContext : IODatabaseContext<DatabaseContext>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
}
