using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface IDotnetMetricsRepository : IRepository<DotnetMetric>
    {
        IList<DotnetMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
