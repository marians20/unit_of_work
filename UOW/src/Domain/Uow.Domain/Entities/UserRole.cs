// <copyright file="Repository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.Domain.Entities;
public sealed class UserRole
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public User User { get; set; }

    public Role Role { get; set; }
}
