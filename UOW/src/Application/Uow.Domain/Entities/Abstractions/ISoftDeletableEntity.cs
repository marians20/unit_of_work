// <copyright file="ISoftDeletableEntity.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.Domain.Entities.Abstractions;

/// <summary>
/// ISoftDeletableEntity
/// </summary>
public interface ISoftDeletableEntity
{
    /// <summary>
    /// The entity is soft deleted
    /// </summary>
    public bool IsDeleted { get; }
}