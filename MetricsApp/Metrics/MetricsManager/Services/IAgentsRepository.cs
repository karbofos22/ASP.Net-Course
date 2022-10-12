using MetricsManager.Agents;

namespace MetricsManager.Services
{
    public interface IAgentsRepository : IRepository<AgentInfo>
    {
        IList<AgentInfo> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
   
}
