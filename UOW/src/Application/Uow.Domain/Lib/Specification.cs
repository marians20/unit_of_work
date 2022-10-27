// <copyright file="Specification.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using System.Linq.Expressions;

namespace Microsoft.Rise.FeedbackService.Core.Lib;

/// <summary>
/// Specification pattern implementation
/// </summary>
/// <typeparam name="T">entity type</typeparam>
public abstract class Specification<T>
{
    /// <summary>
    /// Converts specification to expression
    /// </summary>
    /// <returns></returns>
    public abstract Expression<Func<T, bool>> ToExpression();

    /// <summary>
    /// Check if expression is satisfied by entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool IsSatisfiedBy(T entity) => ToFunc()(entity);

    /// <summary>
    /// Cast Specification{T} to func{T, bool}
    /// </summary>
    /// <returns></returns>
    public Func<T, bool> ToFunc() => ToExpression().Compile();

    /// <summary>
    /// And
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    public Specification<T> And(Specification<T> specification) => new AndSpecification<T>(this, specification);

    /// <summary>
    /// Or
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    public Specification<T> Or(Specification<T> specification) => new OrSpecification<T>(this, specification);

    /// <summary>
    /// Implicit cast to function
    /// </summary>
    /// <param name="spec"></param>
    public static implicit operator Func<T, bool>(Specification<T> spec) => spec.ToFunc();

    /// <summary>
    /// Implicit cast to expression
    /// </summary>
    /// <param name="spec"></param>
    public static implicit operator Expression<Func<T, bool>>(Specification<T> spec) => spec.ToExpression();
}

/// <summary>
/// Implement And operator between specifications
/// </summary>
/// <typeparam name="T">entity type</typeparam>
public class AndSpecification<T> : Specification<T>
{
    private readonly Specification<T> left;
    private readonly Specification<T> right;

    /// <summary>
    /// Initializes a new instance of the <see cref="AndSpecification{T}"/> class.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public AndSpecification(Specification<T> left, Specification<T> right)
    {
        this.right = right;
        this.left = left;
    }

    /// <summary>
    /// cast AndSpecification to expression
    /// </summary>
    /// <returns></returns>
    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = left.ToExpression();
        var rightExpression = right.ToExpression();

        var paramExpr = Expression.Parameter(typeof(T));
        var exprBody = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
        exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
        var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

        return finalExpr;
    }
}

/// <summary>
/// Implement Or operator between specifications
/// </summary>
/// <typeparam name="T">entity type</typeparam>
public class OrSpecification<T> : Specification<T>
{
    private readonly Specification<T> left;
    private readonly Specification<T> right;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrSpecification{T}"/> class.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public OrSpecification(Specification<T> left, Specification<T> right)
    {
        this.right = right;
        this.left = left;
    }

    /// <summary>
    /// Cast OrSpecification to expression
    /// </summary>
    /// <returns></returns>
    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = left.ToExpression();
        var rightExpression = right.ToExpression();
        var paramExpr = Expression.Parameter(typeof(T));
        var exprBody = Expression.OrElse(leftExpression.Body, rightExpression.Body);
        exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
        var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

        return finalExpr;
    }
}

/// <summary>
/// Parameter replacer
/// </summary>
internal class ParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression parameter;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterReplacer"/> class.
    /// </summary>
    /// <param name="parameter"></param>
    internal ParameterReplacer(ParameterExpression parameter) => this.parameter = parameter;

    /// <summary>
    /// VisitParameter
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    protected override Expression VisitParameter(ParameterExpression node) => base.VisitParameter(parameter);
}