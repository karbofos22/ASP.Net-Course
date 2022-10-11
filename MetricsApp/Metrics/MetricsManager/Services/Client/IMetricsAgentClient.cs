using MetricsManager.Models.Requests;

namespace MetricsManager.Services.Client
{
    public interface IMetricsAgentClient
    {
        CpuMetricsResponse GetCpuMetrics(CpuMetricRequest request);
        DotnetMetricsResponse GetDotnetMetrics(DotnetMetricRequest request);
        NetworkMetricsResponse GetNetworkMetrics(NetworkMetricRequest request);
        HddMetricsResponse GetHddMetrics(HddMetricRequest request);
        RamMetricsResponse GetRamMetrics(RamMetricRequest request);
    }
}
