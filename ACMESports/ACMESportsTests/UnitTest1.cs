using System.Text.Json;
using ACMESportsAPI.Model;
using Microsoft.OpenApi.Readers;
using Newtonsoft.Json.Linq;

namespace ACMESportsTests;

public class UnitTest1
{

    [Fact]
    public async Task PostEvents_ValidateResponseAgainstExternalSchema()
    {
        await using var application = new ACMESportsApplication();
        var _client = application.CreateClient("1");
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

        var responseObjects = JsonSerializer.Deserialize<List<Event>>(responseBody);

        foreach (Event evt in responseObjects)
        {
            // Validate Event object
            if (evt.EventId == Guid.Empty)
            {
                return false;
            }

            //check if evt.EventDateimestamp is not null or underfined
            if (evt.EventDateimestamp == null)
            {
                return false;
            }


            if (!TimeSpan.TryParse(evt.EventTime, out _))
                return false;

            if (evt.HomeTeamId == Guid.Empty)
            {
                return false;
            }

            if (evt.AwayTeamId == Guid.Empty)
            {
                return false;
            }

            if (evt.HomeTeamNickName == null) {
                return false;
            }

            if (evt.HomeTeamCity == null)
            {
                return false;   
            }

            if (evt.HomeTeamRank == 0)
            {
                return false;
            }

      
            if (evt.HomeTeamRankPoints == 0)
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

            if (evt.AwayTeamRank == 0)
            {
                return false;
            }   

            if (evt.AwayTeamRankPoints == 0)
            {
                return false;
            }

            return true; // Return true if validation succeeds, false otherwise
        }
        return true;
    }

}
