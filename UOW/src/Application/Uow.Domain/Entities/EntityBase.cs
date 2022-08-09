// <copyright file="EntityBase.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
namespace Uow.Domain.Entities;

public abstract class EntityBase
{
    public Guid Id { get; set; }
}