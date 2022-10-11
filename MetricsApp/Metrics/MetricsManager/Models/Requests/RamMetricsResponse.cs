using System.Text.Json.Serialization;

namespace MetricsManager.Models.Requests
{
    public class RamMetricsResponse
    {
        public int AgentId { get; set; }

        [JsonPropertyName("metrics")]
        public RamMetric[] Metrics { get; set; }
    }
}
