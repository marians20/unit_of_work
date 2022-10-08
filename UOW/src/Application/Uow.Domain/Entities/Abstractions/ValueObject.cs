// <copyright file="ValueObject.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using System.Text.Json;

namespace Uow.Domain.Entities.Abstractions;
public abstract class ValueObject
{
    public static bool operator ==(ValueObject? one, ValueObject? two) => EqualOperator(one, two);

    public static bool operator !=(ValueObject one, ValueObject two) => NotEqualOperator(one, two);

    protected static bool EqualOperator(ValueObject? left, ValueObject? right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
        {
            return false;
        }

        return ReferenceEquals(left, right) || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right) => !(EqualOperator(left, right));

    protected virtual IEnumerable<object> GetEqualityComponents()
    {
        var props = this.GetType().GetProperties();
        foreach (var prop in props)
        {
            yield return JsonSerializer.Serialize(prop.GetValue(this));
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode() =>
        GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
}
