// <copyright file="EntityNotFoundException.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.Domain.Exceptions;

/// <summary>
/// Entity not found exception
/// </summary>
public class EntityNotFoundException : Exception
{

    /// <summary>
    /// Creates a new instance of <see cref="EntityNotFoundException"/>
    /// </summary>
    /// <param name="message"></param>
    public EntityNotFoundException(string message) : base(message)
    {
    }
}
