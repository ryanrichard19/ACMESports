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
//
        // Implement validation logic based on openApiDocument and responseBody
        // ...

        return true; // Return true if validation succeeds, false otherwise
    }

}


