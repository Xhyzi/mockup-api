using System.Text.Json.Serialization;

namespace mockup_api.Dtos
{
    public class DataDto
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("units")]
        public required string Units { get; set; }

        [JsonPropertyName("value")]
        public required int Value { get; set; }

        [JsonPropertyName("extra")]
        public string? Extra { get; set; }
    }
}
