using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {
        IList<NetworkMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
