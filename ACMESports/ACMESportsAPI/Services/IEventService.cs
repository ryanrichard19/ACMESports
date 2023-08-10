using ACMESportsAPI.Models;
using ACMESportsAPI.Models.RequestResponse;

namespace ACMESportsAPI.Services
{
    public interface IEventService
    {
         Task<EventResponse> GetAggregatedEvents(LeagueEnum league, DateTime startDate, DateTime endDate);
    }
}