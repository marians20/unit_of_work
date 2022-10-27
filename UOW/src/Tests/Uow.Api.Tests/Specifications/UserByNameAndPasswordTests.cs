// <copyright file="UserByNameAndPasswordTests.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using FluentAssertions;
using Uow.API.Auth.Models;
using Uow.API.Auth.Specifications;

namespace Uow.Api.Tests.Specifications;
public class UserByNameAndPasswordTests
{
    [Theory]
    [InlineData("name", "password", true)]
    [InlineData("name", "1password", false)]
    [InlineData("name1", "password", false)]
    [InlineData("name1", "1password", false)]
    [InlineData("", "password", false)]
    [InlineData("name", "", false)]
    public void It_ShouldBeSatisfiedByCorrectUserAndPassword(string name, string password, bool shouldPass)
    {
        // arrange
        var user = new User { Id = Guid.NewGuid(), UserName = "name", Password = "password" };

        var sut = new UserByNameAndPassword(name, password);

        // act
        var result = sut.IsSatisfiedBy(user);

        // assert
        result.Should().Be(shouldPass);
    }
}
