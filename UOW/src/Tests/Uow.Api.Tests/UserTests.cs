using FluentAssertions;
using System.Net.Http.Json;
using Uow.Domain.Dtos;
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

            var content = JsonContent.Create<UserDto>(
                new UserDto() { Email = "marian.spoiala@gmail.com"});
            var response = await client.PostAsync("User", content);
            var createdEntityId = await response.Content.ReadAsStringAsync();

            response.IsSuccessStatusCode.Should().BeTrue();

            response = await client.GetAsync("User");
            var body = await response.Content.ReadAsStringAsync();

            response.IsSuccessStatusCode.Should().BeTrue();
            response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");
            body.Should().NotBeNullOrEmpty();
            body.Should().Contain(createdEntityId);
        }
    }
}