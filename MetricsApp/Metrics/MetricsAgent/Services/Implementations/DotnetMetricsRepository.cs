using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.Implementations
{
    public class DotnetMetricsRepository : IDotnetMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _datadaseOptions;
        public DotnetMetricsRepository(IOptions<DatabaseOptions> datadaseOptions)
        {
            _datadaseOptions = datadaseOptions;
        }

        #region Public Methods
        public void Create(DotnetMetric item)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time
                });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);
            connection.Execute("DELETE FROM dotnetmetrics WHERE id=@id",
                new
                {
                    Id = id
                });
        }

        public IList<DotnetMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            return connection.Query<DotnetMetric>("SELECT Id, Time, Value FROM dotnetmetrics").ToList();
        }

        public DotnetMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            return connection.QuerySingle<DotnetMetric>("SELECT Id, Time, Value FROM dotnetmetrics WHERE id = @id",
            new { id = id });
        }

        /// <summary>
        /// Получение данных по нагрузке на ЦП за период
        /// </summary>
        /// <param name="timeFrom">Время начала периода</param>
        /// <param name="timeTo">Время окончания периода</param>
        /// <returns></returns>
        public IList<DotnetMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            return connection.Query<DotnetMetric>("SELECT * FROM dotnetmetrics where time >= @timeFrom and time <= @timeTo",
                new
                {
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();
        }

        public void Update(DotnetMetric item)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            connection.Execute("UPDATE dotnetmetrics SET value = @value, time = @time WHERE id = @id; ",
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
