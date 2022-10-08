// <copyright file="IsUserAdminSpecification.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using System.Linq.Expressions;
using Uow.Domain.Entities;

namespace Uow.Domain.Specifications;
public class IsUserAdminSpecification : Specification<User?>
{
    public override Expression<Func<User?, bool>> ToExpression() =>
        u => u != null && u.Roles.Any(r =>
            r.Name.Equals("admin", StringComparison.OrdinalIgnoreCase)
            || r.Name.Equals("administrator", StringComparison.OrdinalIgnoreCase));
}
