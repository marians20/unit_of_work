// <copyright file="IUnitOfWork.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
namespace Uow.Domain.Contracts;

public interface IUnitOfWork
{
    IRepository Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}