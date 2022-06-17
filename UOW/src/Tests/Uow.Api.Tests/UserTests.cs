using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
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

            var userDto = new UserDto() { Email = "marian.spoiala@gmail.com" };

            var response = await client.PostAsync("User", JsonContent.Create(userDto));
            response.IsSuccessStatusCode.Should().BeTrue();

            var body = await response.Content.ReadAsStringAsync();
            var createdEntityId = JsonConvert.DeserializeObject<Guid>(body);

            createdEntityId.Should().NotBeEmpty();

            userDto.Id = createdEntityId;

            response = await client.GetAsync("User");
            body = await response.Content.ReadAsStringAsync();

            response.IsSuccessStatusCode.Should().BeTrue();
            response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");
            body.Should().NotBeNullOrEmpty();
            
            var actualUserDto = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(body);

            actualUserDto.FirstOrDefault().Should().BeEquivalentTo(userDto);
        }
    }
}