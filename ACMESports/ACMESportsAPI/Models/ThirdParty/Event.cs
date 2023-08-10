using System.Text.Json.Serialization;

namespace ACMESportsAPI.Models.ThirdParty
{
    public class Event
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("home")]
        public TeamInfo Home { get; set; }

        [JsonPropertyName("away")]
        public TeamInfo Away { get; set; }
    }
}
