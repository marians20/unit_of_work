// <copyright file="EntityWithTracking.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.Domain.Entities.Abstractions;

public abstract class EntityWithTracking : EntityBase
{
    public DateTime? CreationDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public void AddCreationTrackingInfo(Guid? userId)
    {
        CreatedBy = userId;
        CreationDate = DateTime.Now;
    }

    public void AddUpdatingTrackingInfo(Guid? userId)
    {
        ModifiedBy = userId;
        ModifiedDate = DateTime.Now;
    }
}
