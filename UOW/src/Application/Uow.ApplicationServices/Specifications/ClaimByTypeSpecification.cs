﻿// <copyright file="ClaimByTypeSpecification.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.Rise.FeedbackService.Core.Lib;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Uow.ApplicationServices.Specifications;
public sealed class ClaimByTypeSpecification : Specification<Claim>
{
    private readonly string claimType;

    public ClaimByTypeSpecification(string claimType) => this.claimType = claimType;

    public override Expression<Func<Claim, bool>> ToExpression() =>
        claim => claim.Type.Equals(claimType);
}
