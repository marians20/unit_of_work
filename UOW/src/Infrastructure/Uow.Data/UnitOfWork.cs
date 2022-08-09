// <copyright file="UnitOfWork.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
using Uow.Domain.Contracts;

namespace Uow.Data;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly UowContext context;

    private readonly IRepository userRepository;

    public UnitOfWork(UowContext context, IRepository userRepository)
    {
        this.context = context;
        Users = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public IRepository Users { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        await context.SaveChangesAsync(cancellationToken);
}