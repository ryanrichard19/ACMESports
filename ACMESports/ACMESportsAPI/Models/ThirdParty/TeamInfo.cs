using System.Text.Json.Serialization;

namespace ACMESportsAPI.Models.ThirdParty
{

    public class TeamInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("nickName")]
        public string NickName { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }
    }
}