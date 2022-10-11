using System.Text.Json.Serialization;

namespace MetricsManager.Models
{
    public class DotnetMetric
    {
        [JsonPropertyName("time")]
        public int Time { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}
