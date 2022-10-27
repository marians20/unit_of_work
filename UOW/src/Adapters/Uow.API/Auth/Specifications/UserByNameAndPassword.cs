// <copyright file="UserByNameAndPassword.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.Rise.FeedbackService.Core.Lib;
using System.Linq.Expressions;
using Uow.API.Auth.Models;

namespace Uow.API.Auth.Specifications;

public sealed class UserByNameAndPassword : Specification<User>
{
    private readonly string userName;
    private readonly string password;

    public UserByNameAndPassword(string userName, string password)
    {
        this.userName = userName;
        this.password = password;
    }

    public override Expression<Func<User, bool>> ToExpression() =>
        x => x.UserName == userName && x.Password == password;
}