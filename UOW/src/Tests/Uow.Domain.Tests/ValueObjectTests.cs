// <copyright file="ValueObjectTests.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using FluentAssertions;
using Uow.Domain.Entities;

namespace Uow.Domain.Tests;

public class ValueObjectTests
{
    [Theory]
    [InlineData("TheCountry", "TheCounty", "City", "123456", "The name of the street and number", "Building and details", true)]
    [InlineData("The Country", "TheCounty", "City", "123456", "The name of the street and number", "Building and details", false)]
    [InlineData("TheCountry", "The County", "City", "123456", "The name of the street and number", "Building and details", false)]
    [InlineData("TheCountry", "TheCounty", "Citty", "123456", "The name of the street and number", "Building and details", false)]
    [InlineData("TheCountry", "TheCounty", "City", "123457", "The name of the street and number", "Building and details", false)]
    [InlineData("TheCountry", "TheCounty", "City", "123456", "The name of the streets and number", "Building and details", false)]
    [InlineData("TheCountry", "TheCounty", "City", "123456", "The name of the street and number", "Building and detail", false)]

    public void ToValueObjects_ShouldBeEqual_WhenAllMembersAreEqual(
        string country,
        string county,
        string city,
        string postalCode,
        string firstLine,
        string secondLine,
        bool equals)
    {
        var a1 = new Address(
            "TheCountry",
            "TheCounty",
            "City",
            "123456",
            "The name of the street and number",
            "Building and details");

        var a2 = new Address(country, county, city, postalCode, firstLine, secondLine);
        if (equals)
        {
            a1.Should().BeEquivalentTo(a2);
        }
        else
        {
            a1.Should().NotBeEquivalentTo(a2);
        }
    }
}