// <copyright file="ArrayExtensions.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
namespace Uow.Data.Extensions;

internal static class ArrayExtensions
{
    /// <summary>
    /// Returns index of string within strings array or -1 if not exists.
    /// </summary>
    /// <param name="strings"></param>
    /// <param name="string"></param>
    /// <returns></returns>
    public static int IndexOf(this string[] strings, string @string)
    {
        for (var i = 0; i < strings.Length; i++)
        {
            if (strings[i].Equals(@string))
            {
                return i;
            }
        }

        return -1;
    }
}