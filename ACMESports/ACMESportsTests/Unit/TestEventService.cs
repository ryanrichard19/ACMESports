using ACMESportsAPI.Models;
using ACMESportsAPI.Models.RequestResponse;
using ACMESportsAPI.Services;

namespace ACMESportsTests.Unit;

public class TestEventService : IEventService
{
    public Task<EventResponse> GetAggregatedEvents(LeagueEnum league, DateTime startDate, DateTime endDate)
    {
        var events = new List<Event>();
        events.Add(
                new Event
                {
                    EventId = Guid.NewGuid(),
                    EventDateimestamp = DateTime.Now,
                    EventTime = "15:30",
                    HomeTeamId = Guid.NewGuid(),
                    HomeTeamNickName = "Hawks",
                    HomeTeamCity = "Hawkstown",
                    HomeTeamRank = 1,
                    HomeTeamRankPoints = 87.5f,
                    AwayTeamId = Guid.NewGuid(),
                    AwayTeamNickName = "Eagles",
                    AwayTeamCity = "Eagleland",
                    AwayTeamRank = 2,
                    AwayTeamRankPoints = 100f
                });

        var sampleEvents = new EventResponse
        {
            Events = events
        };
        return Task.FromResult(sampleEvents);
    }
}
