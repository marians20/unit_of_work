namespace Uow.Tests.Common
{
    public abstract class ApiTestBase
    {
        //protected readonly CustomWebApplicationFactory Application;
        protected readonly HttpClient Client;

        public ApiTestBase()
        {
            //Application = new CustomWebApplicationFactory();
            //Client = Application.CreateClient();
            Client = new CustomWebApplicationFactory().CreateClient();
        }
    }
}