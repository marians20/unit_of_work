// <copyright file="Repository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.API.Services;

public interface IUserResolverService
{
    string? GetUserId();
}