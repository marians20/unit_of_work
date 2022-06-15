using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Uow.Data.Extensions;
using Uow.Domain.Contracts;

namespace Uow.Data;

public static class IoC
{
    public static IServiceCollection RegisterData(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<UowContext>(opts =>
        {
            var connectionString = configuration.GetConnectionString("default").SolveSpecialFolder();

            opts.UseSqlite(connectionString, a =>
            {
                a.MigrationsAssembly(typeof(UowContext).Assembly.FullName);
            });
        })
            .AddScoped<IGenericRepository, GenericRepository<UowContext>>();
}