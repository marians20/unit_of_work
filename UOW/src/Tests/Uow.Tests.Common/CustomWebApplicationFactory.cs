﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Uow.API;
//using Microsoft.VisualStudio.TestPlatform.TestHost;
using Uow.Data;

namespace Uow.Tests.Common
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var scope = builder.ConfigureServices(services => {
                var descriptor =services
                    .SingleOrDefault(d =>d.ServiceType == typeof(DbContextOptions<UowContext>));
                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<UowContext>(options => {
                    //options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseSqlite("Data Source=uow.tests.db");
                });
            });

            using var ctx = new UowContext(new DbContextOptionsBuilder<UowContext>().UseSqlite("Data Source=uow.tests.db").Options);
            ctx.Database.Migrate();
            
        }
    }
}
