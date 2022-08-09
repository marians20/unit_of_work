// <copyright file="User.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
namespace Uow.Domain.Entities;

public class User : EntityWithTracking
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string Salt { get; set; } = default!;

    public DateTime? ExpirationDate { get; set; }

    public bool IsLocked { get; set; } = false;

    public bool IsDeleted { get; set; } = false;

    public bool IsEmailConfirmed { get; set; } = false;

    public ICollection<Role> Roles { get; set; } = new HashSet<Role>();

    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
}