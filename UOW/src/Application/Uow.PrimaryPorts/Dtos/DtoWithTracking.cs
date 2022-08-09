// <copyright file="DtoWithTracking.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Uow.Domain.Entities;

namespace Uow.PrimaryPorts.Dtos;

public abstract class DtoWithTracking : EntityBase
{
    public DateTime? CreationDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? ModifiedBy { get; set; }
}
