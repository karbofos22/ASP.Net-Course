using Dapper;
using MetricsManager.Agents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsManager.Services.Implementations
{
    public class AgentsRepository : IAgentsRepository
    {
        private readonly IOptions<DatabaseOptions> _datadaseOptions;
        public AgentsRepository(IOptions<DatabaseOptions> datadaseOptions)
        {
            _datadaseOptions = datadaseOptions;
        }

        public void Create(AgentInfo item)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            connection.Execute("INSERT INTO Agents(AgentId, AgentAddress, Enable) VALUES(@AgentId, @AgentAddress, @Enable)",
                new
                {
                    AgentId = item.AgentId,
                    AgentAddress = item.AgentAddress,
                    Enable = item.Enable
                });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);
            connection.Execute("DELETE FROM Agents WHERE AgentId=@AgentId",
            new
            {
                    AgentId = id
            });
        }

        public IList<AgentInfo> GetAll()
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);
            IList<AgentInfo> list = connection.Query<AgentInfo>("SELECT AgentId, AgentAddress, Enable FROM Agents").ToList();
            return list;
        }

        public AgentInfo GetById(int id)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            AgentInfo agent = connection.QuerySingle<AgentInfo>("SELECT AgentId, AgentAddress, Enable FROM Agents WHERE AgentId = @AgentId",
            new { AgentId = id });

            return agent;
        }

        public IList<AgentInfo> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            return connection.Query<AgentInfo>("SELECT * FROM Agents where time >= @timeFrom and time <= @timeTo",
                new
                {
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();
        }

        public void Update(AgentInfo item)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            connection.Execute("UPDATE Agents SET AgentAddress = @AgentAddress, Enable = @Enable WHERE AgentId = @AgentId; ",
                new
                {
                    AgentAddress = item.AgentAddress,
                    Enable = item.Enable
                });
        }
    }
}
