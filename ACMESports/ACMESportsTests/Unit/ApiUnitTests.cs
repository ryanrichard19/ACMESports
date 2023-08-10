using System.Text.Json;
using ACMESportsAPI.Models.RequestResponse;
using ACMESportsAPI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Readers;
using NSubstitute;

namespace ACMESportsTests.Unit;

public class ApiUnitTests
{

    [Fact]
    public async Task PostEvents_ValidateResponseAgainstExternalSchema()
    {
        await using var application = new WebApplicationFactory<Program>()
       .WithWebHostBuilder(builder => builder
           .ConfigureServices(services =>
           {
               services.AddSingleton<IEventService, TestEventService>();
           }));

        await using var _sut = new ACMESportsApplication();
        var _client = application.CreateClient();

        // Arrange
        var request = new
        {
            league = "NFL",
            startDate = "2023-08-06",
            endDate = "2023-08-07"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/events", request);
        var responseBody = await response.Content.ReadAsStringAsync();

        // Assert using external schema
        Assert.True(IsValidAgainstOpenApiSchema(responseBody, "openapi.yaml"));
    }

    private bool IsValidAgainstOpenApiSchema(string responseBody, string schemaPath)
    {
        // Load your external OpenAPI schema
        var schemaFile = File.ReadAllText(schemaPath);
        var openApiDocument = new OpenApiStringReader().Read(schemaFile, out var diagnostic);

        if (diagnostic.Errors.Count > 0)
        {
            // Handle OpenAPI schema reading errors
            foreach (var error in diagnostic.Errors)
            {
                Console.WriteLine(error.Message);
            }
            return false;
        }

        var responseObjects = JsonSerializer.Deserialize<EventResponse>(responseBody);

        foreach (Event evt in responseObjects?.Events)
        {
            // Validate Event object
            if (evt.EventId == Guid.Empty)
            {
                return false;
            }

            //check if evt.EventDateimestamp is not null or underfined
            if (evt.EventDateimestamp == DateTime.MinValue)
            {
                return false;
            }


            if (!TimeSpan.TryParse(evt.EventTime, out _))
                return false;

            if (evt.HomeTeamId == Guid.Empty)
            {
                return false;
            }

            if (evt.HomeTeamNickName == null)
            {
                return false;
            }

            if (evt.HomeTeamCity == null)
            {
                return false;
            }

            if (evt.HomeTeamRank < 1)
            {
                return false;
            }

            if (evt.HomeTeamRankPoints < 0)
            {
                return false;
            }

            if (evt.AwayTeamId == Guid.Empty)
            {
                return false;
            }

            if (evt.AwayTeamNickName == null)
            {
                return false;
            }

            if (evt.AwayTeamCity == null)
            {
                return false;
            }

            if (evt.AwayTeamRank < 1)
            {
                return false;
            }

            if (evt.AwayTeamRankPoints < 0)
            {
                return false;
            }
        }
        return true;
    }

}
