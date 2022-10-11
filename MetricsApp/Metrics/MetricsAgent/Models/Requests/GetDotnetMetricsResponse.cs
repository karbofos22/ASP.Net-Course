using MetricsAgent.Models.Dto;

namespace MetricsAgent.Models.Requests
{
    public class GetDotnetMetricsResponse
    {
        public List<DotnetMetricDto> Metrics { get; set; }
    }
}
