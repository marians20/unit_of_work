// <copyright file="TestBase.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Moq;

namespace Uow.Tests.Common;

/// <summary>
/// Base class for unit tests
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class TestBase<T> : IDisposable
{
    protected T Sut;

    protected MockRepository MockRepository { get; }

    protected TestBase()
    {
        MockRepository = new MockRepository(MockBehavior.Strict);

        CreateMocks();

        SetupMocks();
        Sut = CreateSut();
    }

    /// <summary>
    /// Creates the System Under Test
    /// </summary>
    /// <returns></returns>
    protected abstract T CreateSut();

    /// <summary>
    /// Creates the mocks
    /// </summary>
    protected virtual void CreateMocks()
    {
    }

    /// <summary>
    /// provides common setup for mocks
    /// </summary>
    protected virtual void SetupMocks()
    {
        // empty
    }

    public void Dispose()
    {
        MockRepository.VerifyAll();
    }
}
