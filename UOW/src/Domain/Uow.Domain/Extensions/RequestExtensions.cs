using Microsoft.AspNetCore.Http;

namespace Uow.Domain.Extensions;

public static class RequestExtensions
{
    public static string GetRequestId(this HttpRequest request) =>
        request.Headers[Constants.RequestsConstants.Headers.XRequestId];

    public static string CreateRequestIdIfNotExists(this HttpRequest request)
    {
        if (!request.Headers.ContainsKey(Constants.RequestsConstants.Headers.XRequestId))
        {
            var requestId = Guid.NewGuid().ToString();
            request.Headers.Add(Constants.RequestsConstants.Headers.XRequestId, Guid.NewGuid().ToString());
            return requestId;
        }
        else
        {
            return request.Headers[Constants.RequestsConstants.Headers.XRequestId].ToString();
        }
    }
}