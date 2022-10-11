namespace MetricsManager.Models.Requests
{
    public class DotnetMetricRequest
    {
        public int AgentId { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}
