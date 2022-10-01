using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.Implementations
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _datadaseOptions;
        public HddMetricsRepository(IOptions<DatabaseOptions> datadaseOptions)
        {
            _datadaseOptions = datadaseOptions;
        }

        #region Public Methods
        public void Create(HddMetric item)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time
                });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);
            connection.Execute("DELETE FROM hddmetrics WHERE id=@id",
                new
                {
                    Id = id
                });
        }

        public IList<HddMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            return connection.Query<HddMetric>("SELECT Id, Time, Value FROM hddmetrics").ToList();
        }

        public HddMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            return connection.QuerySingle<HddMetric>("SELECT Id, Time, Value FROM hddmetrics WHERE id = @id",
            new { id = id });
        }

        /// <summary>
        /// Получение данных по нагрузке на ЦП за период
        /// </summary>
        /// <param name="timeFrom">Время начала периода</param>
        /// <param name="timeTo">Время окончания периода</param>
        /// <returns></returns>
        public IList<HddMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            return connection.Query<HddMetric>("SELECT * FROM hddmetrics where time >= @timeFrom and time <= @timeTo",
                new
                {
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();
        }

        public void Update(HddMetric item)
        {
            using var connection = new SQLiteConnection(_datadaseOptions.Value.ConnectionString);

            connection.Execute("UPDATE hddmetrics SET value = @value, time = @time WHERE id = @id; ",
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
