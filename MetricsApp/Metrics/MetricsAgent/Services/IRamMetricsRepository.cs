using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {
        IList<RamMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
