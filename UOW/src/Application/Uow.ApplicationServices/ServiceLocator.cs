// <copyright file="ServiceLocator.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace Uow.ApplicationServices;
public static class ServiceLocator
{
    public static IServiceProvider? Provider { set; private get; }

    public static TService GetService<TService>()
    {
        if (Provider == null)
        {
            throw new NullReferenceException(nameof(Provider));
        }

        var result = Provider.GetService<TService>();

        if (result == null)
        {
            throw new NullReferenceException(nameof(TService));
        }

        return result;
    }

    public static IEnumerable<TService> GetServices<TService>()
    {
        if (Provider == null)
        {
            throw new NullReferenceException(nameof(Provider));
        }

        return Provider.GetServices<TService>();
    }
}
