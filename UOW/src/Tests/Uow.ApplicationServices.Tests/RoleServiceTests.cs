// <copyright file="Repository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using Uow.ApplicationServices.Mappers;
using Uow.ApplicationServices.Services;
using Uow.SecondaryPorts;
using Uow.Tests.Common;

namespace Uow.ApplicationServices.Tests;

public class RoleServiceTests : TestBase<RoleService>
{
    private Mock<IRepository> repositoryMock = default!;
    private IMapper mapper  =default!;
    private Mock<IHttpContextAccessor> httpContextAccessorMock = default!;

    protected override void CreateMocks()
    {
        repositoryMock = MockRepository.Create<IRepository>();
        httpContextAccessorMock = MockRepository.Create<IHttpContextAccessor>();

        mapper = new Mapper(new MapperConfiguration(expression => expression.AddProfile(new UowMapperProfile())));
    }

    [Fact]
    public void Test1()
    {

    }

    protected override RoleService CreateSut() => new RoleService(repositoryMock.Object, mapper, httpContextAccessorMock.Object);
}