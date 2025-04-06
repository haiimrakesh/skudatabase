namespace SKUApp.Middleware.Api.IntegrationTests;

using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

        WebApplicationFactory<Program> factory = new CustomWebApplicationFactory<Program>();
        var client = factory.CreateClient();
    }
}

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
    }
}