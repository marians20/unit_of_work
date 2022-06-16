using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Uow.API;
using Uow.Data;

namespace Uow.Api.Tests
{
    public class UserTests
    {
        [Fact]
        public async Task Test1()
        {
            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                // ... Configure test services
                builder.ConfigureServices(sevices => {
                    sevices.AddDbContext<UowContext>(options => {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });
                });
            });

            var client = application.CreateClient();

            var response = await client.GetAsync("User");
            
            response.IsSuccessStatusCode.Should().BeTrue();
            response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");

            var body = await response.Content.ReadAsStringAsync();

            body.Should().NotBeNullOrEmpty();

        }
    }
}