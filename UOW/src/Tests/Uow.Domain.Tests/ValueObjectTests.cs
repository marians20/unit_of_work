// <copyright file="ValueObjectTests.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using FluentAssertions;
using Uow.Domain.Entities;

namespace Uow.Domain.Tests;

public class ValueObjectTests
{
    [Fact]
    public void TowValueObjects_ShouldBeEqual_WhenAllMembersAreEqual()
    {
        var a1 = new Address(
            "Romania",
            "Iasi",
            "Iasi",
            "700588",
            "Piata Voievozilor Nr. 13",
            "Bl.C2, Sc.A Et.1 Ap.3");

        var a2 = new Address(
            "Romania",
            "Iasi",
            "Iasi",
            "700588",
            "Piata Voievozilor Nr. 13",
            "Bl.C2, Sc.A Et.1 Ap.3");

        a1.Should().BeEquivalentTo(a2);
    }

    [Fact]
    public void TowValueObjects_ShouldNotBeEqual_WhenOneMemberDiffers()
    {
        var a1 = new Address(
            "Romania",
            "Iasi",
            "Iasi",
            "700588",
            "Piata Voievozilor Nr. 13",
            "Bl.C2, Sc.A Et.1 Ap.3");

        var a2 = new Address(
            "Romania",
            "Iasi",
            "Iasi",
            "700589",
            "Piata Voievozilor Nr. 13",
            "Bl.C2, Sc.A Et.1 Ap.3");

        a1.Should().NotBeEquivalentTo(a2);
    }
}