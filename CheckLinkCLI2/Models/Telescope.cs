using System.Text.Json.Serialization;

namespace CheckLinkCLI2.Models
{
    public class Telescope
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}