using IOBootstrap.NET.Application;
using IOSwiftUI.Common.Constants;
using IOSwiftUI.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace IOSwiftUI.Application;

public class Startup : IOStartup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(configuration, env)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddDbContext<DatabaseContext>(opt => DatabaseContextOptions((DbContextOptionsBuilder<DatabaseContext>)opt));
    }

    private void DatabaseContextOptions(DbContextOptionsBuilder<DatabaseContext> options)
    {
        string migrationAssembly = Configuration.GetValue<string>(ConfigurationConstants.MigrationsAssemblyKey);
        #if DEBUG
        options.UseLoggerFactory(LoggerFactory.Create(builder =>
        {
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
            .AddConsole();
        }));
        options.EnableSensitiveDataLogging(true);
        #endif

        #if USE_MYSQL_DATABASE
        // options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(migrationAssembly));
        options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(5, 0, 7)), b => b.MigrationsAssembly(migrationAssembly));
        #endif
    }
}
