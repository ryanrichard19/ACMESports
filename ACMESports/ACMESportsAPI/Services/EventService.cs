using ACMESportsAPI.Exceptions;
using ACMESportsAPI.Models;
using ACMESportsAPI.Models.RequestResponse;
using ACMESportsAPI.Models.ThirdParty;
using Microsoft.Extensions.Options;
using Serilog;
using System.Net;
using System.Text.Json;
using System.Web;

namespace ACMESportsAPI.Services
{
    public class EventService : IEventService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public string ApiName { get; } = "ThirdPartyAPI";
        public string BaseUrl { get; }
        private ILogger<EventService> _logger;

        public EventService(IHttpClientFactory httpClientFactory, IOptions<ThirdPartyAPISettings> settings, ILogger<EventService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            BaseUrl = settings.Value.BaseUrl ?? "http://localhost:8000/";
        }

        private async Task<T?> ExecuteRequestAsync<T>(Func<Task<HttpResponseMessage>> httpRequestFunc)
        {
            try
            {
                var response = await httpRequestFunc();
             
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }

                var contentString = await response.Content.ReadAsStringAsync();
                var problem = JsonSerializer.Deserialize<Problem>(contentString);
                HandleErrorResponse(response.StatusCode, problem);
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing the HTTP request");
                throw;

            }
        }

        private void HandleErrorResponse(HttpStatusCode code, Problem? problem)
        {
            switch (code)
            {
                case HttpStatusCode.NotFound:
                    throw new NotFoundException($"Error from Third Party: {problem?.Title}");
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException($"Error from Third Party: {problem?.Title}");
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedAccessException($"Error from Third Party: {problem?.Title}");
                case HttpStatusCode.Forbidden:
                    throw new ForbiddenAccessException($"Error from Third Party: {problem?.Title}");
                default:
                    throw new ApiException($"Error from Third Party: {problem?.Title}");
            }
        }

        private async Task<List<Models.ThirdParty.Event>?> GetScoreboardAsync(string league, DateTime since, DateTime until)
        {
            return await ExecuteRequestAsync<List<Models.ThirdParty.Event>>(() =>
            {
                var builder = new UriBuilder($"{BaseUrl}/{league}/scoreboard");
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["since"] = since.ToString("yyyy-MM-dd");
                query["until"] = until.ToString("yyyy-MM-dd");
                builder.Query = query.ToString();
                var client = _httpClientFactory.CreateClient(ApiName);
                return  client.GetAsync(builder.ToString());
            });
        }

        private async Task<List<Models.ThirdParty.TeamRanking>?> GetTeamRankingsAsync(string league)
        {
            return await ExecuteRequestAsync<List<Models.ThirdParty.TeamRanking>>(() =>
            {
                var builder = new UriBuilder($"{BaseUrl}/{league}/team-rankings");
                var client = _httpClientFactory.CreateClient(ApiName);
                return client.GetAsync(builder.ToString());
            });
        }


        private Models.RequestResponse.Event MapEventResponse(Models.ThirdParty.Event score, List<TeamRanking> teamRankings)
        {
            _logger.LogDebug("Mapping event response for event ID: {EventId}", score.Id);
            var homeTeamRanking = teamRankings.FirstOrDefault(tr => tr.TeamId == score.Home.Id);
            var awayTeamRanking = teamRankings.FirstOrDefault(tr => tr.TeamId == score.Away.Id);
            
            return new Models.RequestResponse.Event
            {
                EventId = score.Id,
                EventDateimestamp = score.Timestamp,
                EventTime = score.Timestamp.TimeOfDay.ToString(),
                HomeTeamId = score.Home.Id,
                HomeTeamNickName = score.Home.NickName,
                HomeTeamCity = score.Home.City ?? string.Empty,
                HomeTeamRank = awayTeamRanking?.Rank ?? 0,
                HomeTeamRankPoints = homeTeamRanking?.RankPoints ?? 0,
                AwayTeamId = score.Away.Id,
                AwayTeamNickName = score.Away.NickName,
                AwayTeamCity = score.Away.City ?? string.Empty,
                AwayTeamRank = awayTeamRanking?.Rank ?? 0,
                AwayTeamRankPoints = awayTeamRanking?.RankPoints ?? 0
            };
        }

        public async Task<EventResponse> GetAggregatedEvents(LeagueEnum league, DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation("Fetching aggregated events for league: {League} from {StartDate} to {EndDate}", league, startDate, endDate);
            var scoreboardTask =  GetScoreboardAsync(league.ToString(), startDate, endDate);
            var teamRankingsTask =  GetTeamRankingsAsync(league.ToString());

            await Task.WhenAll(scoreboardTask, teamRankingsTask);

            var scoreboard = scoreboardTask.Result;
            var teamRankings = teamRankingsTask.Result;

            if (scoreboard == null || !scoreboard.Any())
            {
                throw new InvalidOperationException("No scoreboard data returned.");
            }

            if (teamRankings == null || !teamRankings.Any())
            {
                throw new InvalidOperationException("No team rankings data returned.");
            }


            var eventResponses = scoreboard.Select(score => MapEventResponse(score, teamRankings)).ToList();

            return new EventResponse
            {
                Events = eventResponses
            };
        }


    }

}

