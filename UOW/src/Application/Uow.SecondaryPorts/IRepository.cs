// <copyright file="IRepository.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using System.Linq.Expressions;

namespace Uow.SecondaryPorts;

/// <summary>
/// Generic Repository
/// </summary>
public interface IRepository
{
    /// <summary>
    /// Creates an entity
    /// </summary>
    /// <typeparam name="T">entity type</typeparam>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CreateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Updates an entity
    /// </summary>
    /// <typeparam name="T">entity type</typeparam>
    /// <param name="entity"></param>
    void Update<T>(T entity) where T : class;

    /// <summary>
    /// Deletes an entity
    /// </summary>
    /// <typeparam name="T">entity type</typeparam>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync<T>(Guid id, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Deletes an entity
    /// </summary>
    /// <typeparam name="T">entity type</typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    void Delete<T>(T? entity) where T : class;

    /// <summary>
    /// Deletes a collection of entities
    /// </summary>
    /// <typeparam name="T">entity type </typeparam>
    /// <param name="entities"></param>
    void DeleteRange<T>(IEnumerable<T> entities) where T : class;

    /// <summary>
    /// Gets all entities
    /// </summary>
    /// <typeparam name="T">entity type</typeparam>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> AllAsync<T>(CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Gets an entity by id
    /// </summary>
    /// <typeparam name="T">entity type</typeparam>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T?> GetByIdAsync<T>(object id, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Checks if exists any entity
    /// </summary>
    /// <typeparam name="T">entity type</typeparam>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Gets an IQueryable
    /// </summary>
    /// <typeparam name="T">entity type</typeparam>
    /// <param name="predicate"></param>
    /// <returns></returns>
    IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate) where T : class;

    /// <summary>
    /// Gets an IQueryable including referenced entities
    /// </summary>
    /// <typeparam name="T">entity type</typeparam>
    /// <param name="predicate"></param>
    /// <param name="includeExpressions"></param>
    /// <returns></returns>
    IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions)
        where T : class;

    /// <summary>
    /// Gets an IQueryable including referenced entities
    /// </summary>
    /// <typeparam name="T">entity type</typeparam>
    /// <param name="predicate"></param>
    /// <param name="includePaths"></param>
    /// <returns></returns>
    IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate, params string[] includePaths)
        where T : class;

    /// <summary>
    /// Saves changes to db
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes an action within a transaction
    /// </summary>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteWithinTransactionAsync(Action action, CancellationToken cancellationToken = default!);

    /// <summary>
    /// Executes an async function within a transaction
    /// </summary>
    /// <typeparam name="T">function return type</typeparam>
    /// <param name="func"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T> ExecuteWithinTransactionAsync<T>(Func<Task<T>> func, CancellationToken cancellationToken = default!);
}
