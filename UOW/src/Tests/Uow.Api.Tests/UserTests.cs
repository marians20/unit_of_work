using FluentAssertions;
using Uow.Tests.Common;

namespace Uow.Api.Tests
{
    public class UserTests
    {
        [Fact]
        public async Task Test1()
        {
            var application = new CustomWebApplicationFactory();

            var client = application.CreateClient();

            var response = await client.GetAsync("User");
            
            response.IsSuccessStatusCode.Should().BeTrue();
            response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");

            var body = await response.Content.ReadAsStringAsync();

            body.Should().NotBeNullOrEmpty();

        }
    }
}