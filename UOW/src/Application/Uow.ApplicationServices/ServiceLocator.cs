// <copyright file="ServiceLocator.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace Uow.ApplicationServices;
public static class ServiceLocator
{
    public static IServiceProvider? Provider { set; private get; }

    public static TService? GetService<TService>()
    {
        if (Provider == null)
        {
            throw new NullReferenceException(nameof(Provider));
        }

        return (TService?)Provider.GetService(typeof(TService));
    }

    public static IEnumerable<TService> GetServices<TService>()
    {
        if (Provider == null)
        {
            throw new NullReferenceException(nameof(Provider));
        }

        return (IEnumerable<TService>)Provider.GetServices(typeof(TService));
    }
}
