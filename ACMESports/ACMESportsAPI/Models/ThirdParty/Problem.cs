using System.Text.Json.Serialization;

namespace ACMESportsAPI.Models.ThirdParty
{
    public class Problem
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        [JsonPropertyName("cause")]
        public Problem Cause { get; set; }
    }
}