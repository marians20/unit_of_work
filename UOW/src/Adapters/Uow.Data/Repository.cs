// <copyright file="Repository.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Uow.Domain.Entities.Abstractions;
using Uow.Domain.Extensions;
using Uow.SecondaryPorts;

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

    /// <inheritdoc />
    public async Task CreateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class =>
        await Set<T>().AddAsync(entity, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public void Update<T>(T entity) where T : class =>
        context.Entry(entity).State = EntityState.Modified;

    /// <inheritdoc />
    public async Task DeleteAsync<T>(Guid id, CancellationToken cancellationToken = default) where T : class
    {
        var entity = await GetByIdAsync<T>(id, cancellationToken).ConfigureAwait(false);
        if (entity is null)
        {
            return;
        }

        Set<T>().Remove(entity);
    }

    /// <inheritdoc />
    public void Delete<T>(T? entity) where T : class
    {
        if (entity is null)
        {
            return;
        }

        Set<T>().Remove(entity);
    }

    /// <inheritdoc />
    public void DeleteRange<T>(IEnumerable<T> entities) where T : class => Set<T>().RemoveRange(entities);

    /// <inheritdoc />
    public async Task<IEnumerable<T>> AllAsync<T>(CancellationToken cancellationToken = default) where T : class =>
        typeof(T).IsSoftDeletable()
            ? await Set<T>().Where(e => !((ISoftDeletableEntity)e).IsDeleted).ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false)
            : await Set<T>().ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<T?> GetByIdAsync<T>(object id, CancellationToken cancellationToken = default) where T : class
    {
        var entity = await Set<T>().FindAsync(new[] { id }, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return entity.IsDeleted() ? null : entity;
    }

    /// <inheritdoc />
    public async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class =>
        await Set<T>().AnyAsync(predicate, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate) where T : class =>
        Set<T>().Where(predicate);

    /// <inheritdoc />
    public IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions) where T : class =>
        includeExpressions.Aggregate(
            Set<T>().Where(predicate),
            (current, includeExpression) => current.Include(includeExpression));

    /// <inheritdoc />
    public IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate, params string[] includePaths) where T : class =>
        includePaths.Aggregate(
            Set<T>().Where(predicate),
            (current, includeExpression) => current.Include(includeExpression));

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task ExecuteWithinTransactionAsync(Action action, CancellationToken cancellationToken = default!)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        action.Invoke();
        await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<T> ExecuteWithinTransactionAsync<T>(Func<Task<T>> func, CancellationToken cancellationToken = default!)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        var result = await func.Invoke().ConfigureAwait(false);
        await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }

    private DbSet<T> Set<T>() where T : class => context.Set<T>();
}

