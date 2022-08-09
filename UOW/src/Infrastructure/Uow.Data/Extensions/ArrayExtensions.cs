// <copyright file="ArrayExtensions.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
namespace Uow.Data.Extensions;

internal static class ArrayExtensions
{
    public static int IndexOf(this string[] value, string find)
    {
        for (var i = 0; i < value.Length; i++)
        {
            if (value[i].Equals(find))
            {
                return i;
            }
        }

        return -1;
    }
}