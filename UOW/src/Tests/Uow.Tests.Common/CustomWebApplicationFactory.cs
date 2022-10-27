using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Uow.API;
using Uow.Data;

namespace Uow.Tests.Common;

/// <summary>
/// We are mocking the Db, thus, these are end to end tests
/// We could mock the services and test the routes and controllers only.
/// </summary>
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    const string sqliteTestDbConnectionString = "Data Source=uow.tests.db";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        InitializeDb();

        builder.ConfigureTestServices(services =>
        {
            AddTestDbContext(services);
        });
    }

    private static void InitializeDb()
    {
        var csBuilder = new SqliteConnectionStringBuilder(sqliteTestDbConnectionString);
        var sqliteFileName = csBuilder.DataSource;
        if (File.Exists(sqliteFileName))
        {
            File.Delete(sqliteFileName);
        }

        MigrateDatabase();
    }

    private static void MigrateDatabase()
    {
        var ctx = new UowContext(new DbContextOptionsBuilder<UowContext>()
            .UseSqlite(sqliteTestDbConnectionString).Options);
        ctx.Database.Migrate();
    }

    private static void AddTestDbContext(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<UowContext>));
        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }

        services.AddDbContext<UowContext>(options => { options.UseSqlite(sqliteTestDbConnectionString); });
    }
}
