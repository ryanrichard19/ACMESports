using ACMESportsAPI.Services;

namespace ACMESportsTests;

internal class ACMESportsApplication : WebApplicationFactory<Program>
{
   
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



