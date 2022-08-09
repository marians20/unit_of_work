// <copyright file="DtoBase.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.PrimaryPorts.Dtos;

public abstract class DtoBase
{
    public Guid Id { get; set; }
}