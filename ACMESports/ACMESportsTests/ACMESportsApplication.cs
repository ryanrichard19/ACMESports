using Microsoft.AspNetCore.Hosting;

namespace ACMESportsTests;

internal class ACMESportsApplication : WebApplicationFactory<Program>
{

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            configBuilder.AddInMemoryCollection(new Dictionary<string, string>
        {
            { "ThirdPartyAPI:BaseUrl", "http://localhost:4010" }
        });
        });
    }

    public HttpClient CreateClient(string id)
    {
        return CreateDefaultClient();
    }
    protected override IHost CreateHost(IHostBuilder builder)
    {
        return base.CreateHost(builder);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }
}



