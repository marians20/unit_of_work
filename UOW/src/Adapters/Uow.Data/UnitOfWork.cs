// <copyright file="UnitOfWork.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Uow.SecondaryPorts;

namespace Uow.Data;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly UowContext context;

    public UnitOfWork(UowContext context) => this.context = context;

    public IRepository Users { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        await context.SaveChangesAsync(cancellationToken);
}