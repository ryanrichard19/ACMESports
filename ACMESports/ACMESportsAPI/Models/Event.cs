/*
 * Medida Challenge API
 *
 * This is the API that must be implemented as a your output deliverable of this challenge
 *
 * The version of the OpenAPI document: 0.1.0
 * Contact: challenges@medida.com
 * Generated by: https://openapi-generator.tech
 */


using System.Text.Json.Serialization;

namespace ACMESportsAPI.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Gets or Sets EventId
        /// </summary>
        [JsonPropertyName("eventId")]
        public Guid EventId { get; set; }

        /// <summary>
        /// Gets or Sets EventDateimestamp
        /// </summary>
        [JsonPropertyName("eventDateimestamp")]
        public DateTime EventDateimestamp { get; set; }

        /// <summary>
        /// Gets or Sets EventTime
        /// </summary>
        [JsonPropertyName("eventTime")]
        public string EventTime { get; set; }

        /// <summary>
        /// Gets or Sets HomeTeamId
        /// </summary>
        [JsonPropertyName("homeTeamId")]
        public Guid HomeTeamId { get; set; }

        /// <summary>
        /// Gets or Sets HomeTeamNickName
        /// </summary>
        [JsonPropertyName("homeTeamNickName")]
        public string HomeTeamNickName { get; set; }

        /// <summary>
        /// Gets or Sets HomeTeamCity
        /// </summary>
        [JsonPropertyName("homeTeamCity")]
        public string HomeTeamCity { get; set; }

        /// <summary>
        /// Gets or Sets HomeTeamRank
        /// </summary>
        [JsonPropertyName("homeTeamRank")]
        public long HomeTeamRank { get; set; }

        /// <summary>
        /// Gets or Sets HomeTeamRankPoints
        /// </summary>
        [JsonPropertyName("homeTeamRankPoints")]
        public float HomeTeamRankPoints { get; set; }

        /// <summary>
        /// Gets or Sets AwayTeamId
        /// </summary>
        [JsonPropertyName("awayTeamId")]
        public Guid AwayTeamId { get; set; }

        /// <summary>
        /// Gets or Sets AwayTeamNickName
        /// </summary>
        [JsonPropertyName("awayTeamNickName")]
        public string AwayTeamNickName { get; set; }

        /// <summary>
        /// Gets or Sets AwayTeamCity
        /// </summary>
        [JsonPropertyName("awayTeamCity")]
        public string AwayTeamCity { get; set; }

        /// <summary>
        /// Gets or Sets AwayTeamRank
        /// </summary>
        [JsonPropertyName("awayTeamRank")]
        public long AwayTeamRank { get; set; }

        /// <summary>
        /// Gets or Sets AwayTeamRankPoints
        /// </summary>
        [JsonPropertyName("awayTeamRankPoints")]
        public float AwayTeamRankPoints { get; set; }
    }

}