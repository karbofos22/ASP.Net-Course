using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.Implementations
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _datadaseOptions;
        public RamMetricsRepository(IOptions<DatabaseOptions> datadaseOptions)
        {
            _datadaseOptions = datadaseOptions;
        }

        #region Public Methods
        public void Create(RamMetric item)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time
                });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);
            connection.Execute("DELETE FROM rammetrics WHERE id=@id",
                new
                {
                    Id = id
                });
        }

        public IList<RamMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            return connection.Query<RamMetric>("SELECT Id, Time, Value FROM rammetrics").ToList();
        }

        public RamMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            return connection.QuerySingle<RamMetric>("SELECT Id, Time, Value FROM rammetrics WHERE id = @id",
            new { id = id });
        }

        /// <summary>
        /// Получение данных по нагрузке на ЦП за период
        /// </summary>
        /// <param name="timeFrom">Время начала периода</param>
        /// <param name="timeTo">Время окончания периода</param>
        /// <returns></returns>
        public IList<RamMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            return connection.Query<RamMetric>("SELECT * FROM rammetrics where time >= @timeFrom and time <= @timeTo",
                new
                {
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();
        }

        public void Update(RamMetric item)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            connection.Execute("UPDATE rammetrics SET value = @value, time = @time WHERE id = @id; ",
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
