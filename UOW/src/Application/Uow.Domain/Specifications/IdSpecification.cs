// <copyright file="IdSpecification.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using System.Linq.Expressions;
using Uow.Domain.Entities.Abstractions;

namespace Uow.Domain.Specifications;
public class IdSpecification : Specification<EntityBase>
{
    private readonly Guid id;

    public IdSpecification(Guid id) => this.id = id;

    public override Expression<Func<EntityBase, bool>> ToExpression() =>
        u => u.Id == id;
}
