// <copyright file="Repository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.PrimaryPorts.Dtos;
public class RoleDto: DtoWithTracking
{
    public string? Name { get; set; }

    public string? Description { get; set; }
}
