using System.Text.Json.Serialization;

namespace MetricsManager.Models.Requests
{
    public class HddMetricsResponse
    {
        public int AgentId { get; set; }

        [JsonPropertyName("metrics")]
        public HddMetric[] Metrics { get; set; }
    }
}
