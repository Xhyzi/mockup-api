using System.Text.Json.Serialization;

namespace mockup_api.Dtos
{
    public class PayloadDto
    {
        [JsonPropertyName("code")]
        public required string Code { get; set; }

        [JsonPropertyName("timestamp")]
        public required int Timestamp { get; set; }

        public required List<DataDto> Data { get; set; }
    }
}
