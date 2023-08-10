using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ACMESportsAPI.Models.RequestResponse
{
    public class EventResponse
    {
        [Required]
        [JsonPropertyName("EventResponse")]

        public List<Event> Events { get; set; }

    }
}




