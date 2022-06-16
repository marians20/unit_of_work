using Microsoft.AspNetCore.Http;

namespace Uow.Domain.Extensions;

public static class RequestExtensions
{
    private const string XRequestId = "x-request_id";
    public static string GetRequestId(this HttpRequest request) =>
        request.Headers[XRequestId];

    public static void CreateRequestIdIfNotExists(this HttpRequest request)
    {
        if (!request.Headers.ContainsKey(XRequestId))
        {
            request.Headers.Add(XRequestId, Guid.NewGuid().ToString());
        }
    }
}