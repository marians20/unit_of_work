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
            var body = await response.Content.ReadAsStringAsync();

            response.IsSuccessStatusCode.Should().BeTrue();
            response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");
            body.Should().NotBeNullOrEmpty();

        }
    }
}