using System.Text;
using System.Text.Json;
using ACMESportsAPI.Models.RequestResponse;
using ACMESportsAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Readers;


namespace ACMESportsTests.Integration;

public class IntegrationTests
{

    private readonly ACMESportsApplication _factory;
    private readonly HttpClient _client;

    public IntegrationTests()
    {
        _factory = new ACMESportsApplication();
        _client = _factory.CreateClient();
    }



    [Fact]
    public async Task GetEvents_ReturnsSuccessStatusCode()
    {
        var response = await _client.PostAsync("/events", new StringContent(JsonSerializer.Serialize(new
        {
            league = "NFL",
            startDate = "2023-08-01",
            endDate = "2023-08-31"
        }), Encoding.UTF8, "application/json"));

        // Assert
        Assert.True(response.IsSuccessStatusCode);
    }


    [Fact]
    public async Task GetEvents_ReturnsErrorForInvalidInput()
    {
        var response = await _client.PostAsync("/events", new StringContent(JsonSerializer.Serialize(new
        {
            league = "INVALID_LEAGUE",
            startDate = "2023-08-01",
            endDate = "2023-08-31"
        }), Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
