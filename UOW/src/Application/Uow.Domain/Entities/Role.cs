﻿// <copyright file="Role.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Uow.Domain.Entities.Abstractions;

namespace Uow.Domain.Entities;
public class Role: EntityWithTracking
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public virtual ICollection<User> Users { get; set; } = new HashSet<User>();

    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
}
