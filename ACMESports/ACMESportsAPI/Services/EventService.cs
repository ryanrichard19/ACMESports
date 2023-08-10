using ACMESportsAPI.Exceptions;
using ACMESportsAPI.Models;
using ACMESportsAPI.Models.RequestResponse;
using ACMESportsAPI.Models.ThirdParty;
using System.Net;
using System.Text.Json;
using System.Web;

namespace ACMESportsAPI.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;
        private const string API_NAME = "ThirdPartyAPI";

        public EventService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(API_NAME);
        }

        private async Task<T> ExecuteRequestAsync<T>(Func<Task<HttpResponseMessage>> httpRequestFunc)
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

        private void HandleErrorResponse(HttpStatusCode code, Problem problem)
        {
            switch (code)
            {
                case HttpStatusCode.NotFound:
                    throw new NotFoundException($"Error from Third Party: {problem.Title}");
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException($"Error from Third Party: {problem.Title}");
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedAccessException($"Error from Third Party: {problem.Title}");
                case HttpStatusCode.Forbidden:
                    throw new ForbiddenAccessException($"Error from Third Party: {problem.Title}");
                default:
                    throw new ApiException($"Error from Third Party: {problem.Title}");
            }
        }

        private async Task<List<Models.ThirdParty.Event>> GetScoreboardAsync(string league, DateTime since, DateTime until)
        {
            return await ExecuteRequestAsync<List<Models.ThirdParty.Event>>(() =>
            {
                var builder = new UriBuilder($"{_httpClient.BaseAddress}{league}/scoreboard");
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["since"] = since.ToString("yyyy-MM-dd");
                query["until"] = until.ToString("yyyy-MM-dd");
                builder.Query = query.ToString();
                return _httpClient.GetAsync(builder.ToString());
            });
        }

        private async Task<List<Models.ThirdParty.TeamRanking>> GetTeamRankingsAsync(string league)
        {
            return await ExecuteRequestAsync<List<Models.ThirdParty.TeamRanking>>(() =>
            {
                var builder = new UriBuilder($"{_httpClient.BaseAddress}{league}/team-rankings");
                return _httpClient.GetAsync(builder.ToString());
            });
        }


        private Models.RequestResponse.Event MapEventResponse(Models.ThirdParty.Event score, List<TeamRanking> teamRankings)
        {
            var homeTeamRanking = teamRankings.FirstOrDefault(tr => tr.TeamId == score.Home.Id);
            var awayTeamRanking = teamRankings.FirstOrDefault(tr => tr.TeamId == score.Away.Id);

            return new Models.RequestResponse.Event
            {
                EventId = score.Id,
                EventDateimestamp = score.Timestamp,
                EventTime = score.Timestamp.TimeOfDay.ToString(),
                HomeTeamId = score.Home.Id,
                HomeTeamNickName = score.Home.NickName,
                HomeTeamCity = score.Home.City,
                HomeTeamRank = awayTeamRanking?.Rank ?? 0,
                HomeTeamRankPoints = homeTeamRanking?.RankPoints ?? 0,
                AwayTeamId = score.Away.Id,
                AwayTeamNickName = score.Away.NickName,
                AwayTeamCity = score.Away.City,
                AwayTeamRank = awayTeamRanking?.Rank ?? 0,
                AwayTeamRankPoints = awayTeamRanking?.RankPoints ?? 0
            };
        }

        public async Task<EventResponse> GetAggregatedEvents(LeagueEnum league, DateTime startDate, DateTime endDate)
        {
            var scoreboard = await GetScoreboardAsync(league.ToString(), startDate, endDate);
            var teamRankings = await GetTeamRankingsAsync(league.ToString());

            var eventResponses = scoreboard.Select(score => MapEventResponse(score, teamRankings)).ToList();

            return new EventResponse
            {
                Events = eventResponses
            };
        }


    }

}

