namespace Uow.Tests.Common;

public abstract class ApiTestBase
{
    protected readonly HttpClient Client;

    protected ApiTestBase() => Client = new CustomWebApplicationFactory().CreateClient();
}