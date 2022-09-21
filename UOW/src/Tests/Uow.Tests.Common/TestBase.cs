// <copyright file="Repository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Moq;

namespace Uow.Tests.Common;
public abstract class TestBase<TSut> : IDisposable
{
    protected MockRepository MockRepository;

    protected TSut Sut = default!;

    protected TestBase()
    {
        MockRepository = new MockRepository(MockBehavior.Strict);

        CreateMocks();

        SetupMocks();
    }

    protected abstract TSut CreateSut();

    protected abstract void CreateMocks();

    protected virtual void SetupMocks()
    {
        // empty
    }

    public void Dispose() => MockRepository.VerifyAll();
}
