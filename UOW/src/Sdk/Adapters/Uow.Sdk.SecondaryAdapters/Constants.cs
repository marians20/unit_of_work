// <copyright file="Constants.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>


namespace Uow.Sdk.SecondaryAdapters;
internal static class Constants
{
    /// <summary>
    /// Various constants
    /// </summary>
    public static class HttpConstants
    {
        /// <summary>
        /// Accept header
        /// </summary>
        public const string AcceptHeader = "Accept";

        /// <summary>
        /// Authorization header
        /// </summary>
        public const string AuthorizationHeader = "Authorization";

        /// <summary>
        /// Bearer
        /// </summary>
        public const string Bearer = "Bearer";

        /// <summary>
        /// Accept header values
        /// </summary>
        public static class AcceptHeaderValues
        {
            /// <summary>
            /// application/json
            /// </summary>
            public const string Json = "application/json";
        }
    }

    /// <summary>
    /// EndpointConstants
    /// </summary>
    public static class EndpointConstants
    {
        /// <summary>
        /// Client Applications
        /// </summary>
        public const string ClientApplications = "ClientApplications";
    }

    public static class InternalConstants
    {
        public const string InjectedHttpClientName = "Uow.Sdk.SecondaryPorts.ApiClient";
    }
}
