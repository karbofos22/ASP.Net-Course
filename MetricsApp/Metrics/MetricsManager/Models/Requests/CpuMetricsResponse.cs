using System.Text.Json.Serialization;

namespace MetricsManager.Models.Requests
{
    public class CpuMetricsResponse
    {
        public int AgentId { get; set; }

        [JsonPropertyName("metrics")]
        public CpuMetric[] Metrics {get; set;}

    }
}
