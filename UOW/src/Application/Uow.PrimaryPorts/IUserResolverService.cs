// <copyright file="Repository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.PrimaryPorts;

public interface IUserResolverService
{
    string? GetUserId();
}