using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.Implementations
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _datadaseOptions;
        public NetworkMetricsRepository(IOptions<DatabaseOptions> datadaseOptions)
        {
            _datadaseOptions = datadaseOptions;
        }

        #region Public Methods
        public void Create(NetworkMetric item)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time
                });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);
            connection.Execute("DELETE FROM networkmetrics WHERE id=@id",
                new
                {
                    Id = id
                });
        }

        public IList<NetworkMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            return connection.Query<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics").ToList();
        }

        public NetworkMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            return connection.QuerySingle<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics WHERE id = @id",
            new { id = id });
        }

        /// <summary>
        /// Получение данных по нагрузке на ЦП за период
        /// </summary>
        /// <param name="timeFrom">Время начала периода</param>
        /// <param name="timeTo">Время окончания периода</param>
        /// <returns></returns>
        public IList<NetworkMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics where time >= @timeFrom and time <= @timeTo",
                new
                {
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();
        }

        public void Update(NetworkMetric item)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            connection.Execute("UPDATE networkmetrics SET value = @value, time = @time WHERE id = @id; ",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
        }
        #endregion
    }
}
