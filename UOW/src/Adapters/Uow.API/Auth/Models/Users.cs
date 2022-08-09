// <copyright file="User.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
namespace Uow.API.Auth.Models;

public class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = default!;

    public string EmailId { get; set; } = default!;

    public string Password { get; set; } = default!;
}