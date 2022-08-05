using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Uow.Domain.Dtos;
using Uow.Tests.Common;
// ReSharper disable PossibleMultipleEnumeration
#pragma warning disable CS8604 // Possible null reference argument.
namespace Uow.Api.Tests;

public class UserTests: ApiTestBase
{
    [Fact]
    public async Task Test1()
    {
        var userDto = new UserDto() { Email = "john.smith@gmail.com" };

        var response = await Client.PostAsync("User", JsonContent.Create(userDto));
        response.IsSuccessStatusCode.Should().BeTrue();

        var body = await response.Content.ReadAsStringAsync();
        var createdEntityId = JsonConvert.DeserializeObject<Guid>(body);

        createdEntityId.Should().NotBeEmpty();

        userDto.Id = createdEntityId;

        response = await Client.GetAsync("User");
        body = await response.Content.ReadAsStringAsync();

        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");
        
        body.Should().NotBeNullOrEmpty();
        
        var actualUsers = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(body);

        actualUsers.Should().NotBeNullOrEmpty();
        actualUsers.FirstOrDefault().Should().BeEquivalentTo(userDto);
    }
}

#pragma warning restore CS8604 // Possible null reference argument.