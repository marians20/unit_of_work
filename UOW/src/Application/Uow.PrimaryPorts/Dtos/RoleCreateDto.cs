// <copyright file="RoleCreateDto.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.PrimaryPorts.Dtos;
public sealed class RoleCreateDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }
}
