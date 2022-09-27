using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface IHddMetricsRepository : IRepository<HddMetric>
    {
        IList<HddMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
