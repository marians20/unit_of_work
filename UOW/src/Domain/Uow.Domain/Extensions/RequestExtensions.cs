using Microsoft.AspNetCore.Http;

namespace Uow.Domain.Extensions;

public static class RequestExtensions
{
    private const string XRequestId = "x-request_id";
    public static string GetRequestId(this HttpRequest request) =>
        request.Headers[XRequestId];

    public static string CreateRequestIdIfNotExists(this HttpRequest request)
    {
        if (!request.Headers.ContainsKey(XRequestId))
        {
            var requestId = Guid.NewGuid().ToString();
            request.Headers.Add(XRequestId, Guid.NewGuid().ToString());
            return requestId;
        }
        else
        {
            return request.Headers[XRequestId].ToString();
        }
    }
}