// <copyright file="Repository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Uow.API.Auth.Models;

namespace Uow.API.Auth.Services;

public interface IAuthService
{
    UserTokens? GetToken(UserLogin userLogin);
}