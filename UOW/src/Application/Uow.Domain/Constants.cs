// <copyright file="Repository.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Uow.Domain;
public static class Constants
{
    public static class RequestsConstants
    {
        public static class Headers
        {
            public const string XRequestId = "x-request_id";
            public const string Culture = "culture";
        }
    }

    public static class Claims
    {
        public const string Email = "email";
        public const string Id = "id";
    }
}
