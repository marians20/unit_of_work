// <copyright file="Repository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.Domain.Dtos;
public sealed class UserCreateDto
{
    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? ExpirationDate { get; set; }
}
