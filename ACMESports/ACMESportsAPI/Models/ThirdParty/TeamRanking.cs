using System.Text.Json.Serialization;

namespace ACMESportsAPI.Models.ThirdParty
{

    public class TeamRanking
    {
        [JsonPropertyName("teamId")]
        public Guid TeamId { get; set; }

        [JsonPropertyName("rank")]
        public long Rank { get; set; }

        [JsonPropertyName("rankPoints")]
        public float RankPoints { get; set; }
    }
}