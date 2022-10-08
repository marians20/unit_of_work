// <copyright file="Repository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Uow.Domain.Entities.Abstractions;

namespace Uow.Domain.Extensions;
/// <summary>
/// Reflection Extensions
/// </summary>
public static class ReflectionExtensions
{
    /// <summary>
    /// Check if the passed entity implements ISoftDeletable interface
    /// </summary>
    /// <param name="entityType"></param>
    /// <returns></returns>
    public static bool IsSoftDeletable(this Type entityType) => entityType.GetInterfaces().Any(i => i == typeof(ISoftDeletableEntity));

    /// <summary>
    /// IsDeleted
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static bool IsDeleted(this object? entity)
    {
        var softDeletableEntity = entity as ISoftDeletableEntity;

        return softDeletableEntity?.IsDeleted ?? false;
    }
}
