using IOBootstrap.NET.Application;
using IOSwiftUI.Application.Filters;
using IOSwiftUI.Common.Constants;
using IOSwiftUI.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IOSwiftUI.Application;

public class Startup : IOStartup<DatabaseContext>
{
    public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(configuration, env)
    {
    }

    public override void ConfigureSwagger(SwaggerGenOptions options)
    {
        options.OperationFilter<DefaultHeaderFilter>();
    }
    
    public override void DatabaseContextOptions(DbContextOptionsBuilder<DatabaseContext> options)
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
        #elif USE_SQLSRV_DATABASE
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(migrationAssembly));
        #else
        options.UseInMemoryDatabase("IOMemory");
        #endif
    }
    
}
