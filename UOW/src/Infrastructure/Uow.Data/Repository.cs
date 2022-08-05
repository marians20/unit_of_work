// <copyright file="Repository.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Uow.Domain.Contracts;

namespace Uow.Data;

/// <summary>
/// Generic Repository
/// </summary>
/// <typeparam name="TContext">DbContext type</typeparam>
internal class Repository<TContext> : IRepository where TContext : DbContext
{
    private readonly TContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TContext}"/> class.
    /// </summary>
    /// <param name="context"></param>
    public Repository(TContext context) => this.context = context;

    /// <inherritdoc />
    public async Task CreateAsync<T>(T entity) where T : class =>
        await Set<T>().AddAsync(entity).ConfigureAwait(false);

    /// <inherritdoc />
    public void Update<T>(T entity) where T : class =>
        context.Entry(entity).State = EntityState.Modified;

    /// <inherritdoc />
    public async Task DeleteAsync<T>(Guid id, CancellationToken cancellationToken = default) where T : class
    {
        var entity = await GetByIdAsync<T>(id, cancellationToken).ConfigureAwait(false);
        if (entity is null)
        {
            return;
        }

        Set<T>().Remove(entity);
    }

    /// <inherritdoc />
    public async Task<IEnumerable<T>> AllAsync<T>(CancellationToken cancellationToken = default) where T : class =>
        await Set<T>().ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

    /// <inherritdoc />
    public async Task<T?> GetByIdAsync<T>(Guid id, CancellationToken cancellationToken = default) where T : class =>
        await Set<T>().FindAsync(new object?[] { id }, cancellationToken: cancellationToken).ConfigureAwait(false);

    /// <inherritdoc />
    public async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class =>
        await Set<T>().AnyAsync(predicate, cancellationToken).ConfigureAwait(false);

    /// <inherritdoc />
    public IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate) where T : class =>
        Set<T>().Where(predicate);

    /// <inherritdoc />
    public IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions) where T : class =>
        includeExpressions.Aggregate(
            Set<T>().Where(predicate),
            (current, includeExpression) => current.Include(includeExpression));

    /// <inherritdoc />
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    /// <inherritdoc />
    public async Task ExecuteWithinTransactionAsync(Action action, CancellationToken cancellationToken = default!)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        action.Invoke();
        await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inherritdoc />
    public async Task<T> ExecuteWithinTransactionAsync<T>(Func<Task<T>> func, CancellationToken cancellationToken = default!)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        var result = await func.Invoke().ConfigureAwait(false);
        await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }

    private DbSet<T> Set<T>() where T : class => context.Set<T>();
}

