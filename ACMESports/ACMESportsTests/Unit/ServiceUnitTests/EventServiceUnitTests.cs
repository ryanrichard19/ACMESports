using ACMESportsAPI.Exceptions;
using ACMESportsAPI.Models;
using ACMESportsAPI.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Text;
using System.Text.Json;

namespace ACMESportsTests.Unit.ServiceUnitTests
{

    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly Dictionary<string, HttpResponseMessage> _urlToResponseMap;

        public FakeHttpMessageHandler(Dictionary<string, HttpResponseMessage> urlToResponseMap)
        {
            _urlToResponseMap = urlToResponseMap;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_urlToResponseMap.ContainsKey(request.RequestUri.ToString()))
            {
                return Task.FromResult(_urlToResponseMap[request.RequestUri.ToString()]);
            }

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
    }


    public class EventServiceUnitTests : IDisposable
    {

        private string BASE_URL = string.Empty;
        private IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        private IConfiguration _configuration;
        private ILogger<EventService> _logger;


        public EventServiceUnitTests()
        {
            _httpClientFactory = Substitute.For<IHttpClientFactory>();
            _configuration = Substitute.For<IConfiguration>();
            _logger = Substitute.For<ILogger<EventService>>();

            _configuration["BASE_URL"].Returns("http://localhost:8000");
            BASE_URL = _configuration["BASE_URL"];

            
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        private EventService CreateService(Dictionary<string, HttpResponseMessage> urlToResponseMap)
        {
            _logger.Received(1).LogDebug(Arg.Any<string>());
            _logger.Received(1).LogInformation(Arg.Any<string>());
            _logger.Received(1).LogError(Arg.Any<string>());
            _logger.Received(1).LogWarning(Arg.Any<string>());
            _logger.Received(1).LogInformation(Arg.Is<string>(s => s.Contains("Made a request to")), Arg.Any<Uri>(), Arg.Any<HttpStatusCode>());

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(urlToResponseMap);
            _httpClient = new HttpClient(fakeHttpMessageHandler)
            {
                BaseAddress = new Uri(BASE_URL)
            };

            _httpClientFactory.CreateClient("ThirdPartyAPI").Returns(_httpClient);
            _configuration.GetValue<string>("BASE_URL").Returns(BASE_URL);

            return new EventService(_httpClientFactory, _configuration, _logger);
        }

        private string ReadJsonFromFile(string filePath)
        {
            using var r = new StreamReader(filePath);
            return r.ReadToEnd();
        }
        private EventService SetupService(Dictionary<string, HttpResponseMessage> urlToResponseMap)
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(urlToResponseMap);
            var httpClient = new HttpClient(fakeHttpMessageHandler) { BaseAddress = new Uri(BASE_URL) };
            _configuration.GetValue<string>("BASE_URL").Returns(BASE_URL);
            var httpClientFactory = Substitute.For<IHttpClientFactory>();
            httpClientFactory.CreateClient("ThirdPartyAPI").Returns(httpClient);

            return new EventService(httpClientFactory, _configuration, _logger);
        }

        private Dictionary<string, HttpResponseMessage> CreateErrorResponseMap(HttpStatusCode statusCode, string errorMessage, params string[] urls)
        {
            var problemJson = $"{{ \"title\": \"{errorMessage}\" }}";
            var errorResponse = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(problemJson, Encoding.UTF8, "application/json")
            };
            return urls.ToDictionary(url => url, url => errorResponse);
        }


  

        [Fact]
        public async Task GetAggregatedEvents_ValidateResponseAgainstExternalSchema()
        {

            // Arrange
            var scoreboardJson = ReadJsonFromFile("Unit\\scoreboardTestData.json");
            var teamRankingsJson = ReadJsonFromFile("Unit\\teamRankingsTestData.json");
            var league = LeagueEnum.NFL;
            var startDate = new DateTime(2023, 08, 06);
            var endDate = new DateTime(2023, 08, 07);

            var urlToResponseMap = new Dictionary<string, HttpResponseMessage>
    {
        { $"{BASE_URL}/{league}/scoreboard?since={startDate:yyyy-MM-dd}&until={endDate:yyyy-MM-dd}", new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(scoreboardJson) } },
        { $"{BASE_URL}/{league}/team-rankings", new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(teamRankingsJson) } }
    };

            var service = SetupService(urlToResponseMap);

            // Act
            var response = await service.GetAggregatedEvents(league, startDate, endDate);

            // Assert
            Assert.NotNull(response);

            var scoreboardData =  JsonSerializer.Deserialize<List<ACMESportsAPI.Models.ThirdParty.Event>>(scoreboardJson);
            Assert.Equal(scoreboardData.Count, response.Events.Count);

            for (int i = 0; i < response.Events.Count; i++)
            {
                var actualEvent = response.Events[i];
                var expectedEvent = scoreboardData[i];

                // Validate event-level attributes
                Assert.Equal(expectedEvent.Id, actualEvent.EventId);
                Assert.Equal(expectedEvent.Timestamp, actualEvent.EventDateimestamp);

                // Validate home team attributes
                Assert.Equal(expectedEvent.Home.Id, actualEvent.HomeTeamId);
                Assert.Equal(expectedEvent.Home.NickName, actualEvent.HomeTeamNickName);
                Assert.Equal(expectedEvent.Home.City, actualEvent.HomeTeamCity);

                // Validate away team attributes
                Assert.Equal(expectedEvent.Away.Id, actualEvent.AwayTeamId);
                Assert.Equal(expectedEvent.Away.NickName, actualEvent.AwayTeamNickName);
                Assert.Equal(expectedEvent.Away.City, actualEvent.AwayTeamCity);

                
            }
        }

        [Theory]
        [InlineData(HttpStatusCode.BadRequest, typeof(BadRequestException), "Bad request error message")]
        [InlineData(HttpStatusCode.Unauthorized, typeof(UnauthorizedAccessException), "Unauthorized error message")]
        [InlineData(HttpStatusCode.Forbidden, typeof(ForbiddenAccessException), "Forbidden error message")]
        public async Task Service_ShouldThrowCorrectException_WhenAPIReturnsError(HttpStatusCode statusCode, Type exceptionType, string errorMessage)
        {
            // Arrange
            var errorMap = CreateErrorResponseMap(
                statusCode,
                errorMessage,
                $"{BASE_URL}/NFL/scoreboard?since=2023-08-06&until=2023-08-07",
                $"{BASE_URL}/NFL/team-rankings"
            );
            var service = SetupService(errorMap);

            var exception = await Assert.ThrowsAsync(exceptionType, async () =>
    await service.GetAggregatedEvents(LeagueEnum.NFL, new DateTime(2023, 08, 06), new DateTime(2023, 08, 07)));

            Assert.Equal($"Error from Third Party: {errorMessage}", exception.Message);
        }





    }
}
