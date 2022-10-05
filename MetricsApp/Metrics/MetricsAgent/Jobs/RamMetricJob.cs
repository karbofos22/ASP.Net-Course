using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsRepository _ramMetricsRepository;
        private PerformanceCounter _ramCounter;

        public RamMetricJob(IRamMetricsRepository ramMetricsRepository)
        {
            _ramMetricsRepository = ramMetricsRepository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes", null);
        }
        public Task Execute(IJobExecutionContext context)
        {
            // Получаем значение занятости CPU
            var ramUsageInPercents = Convert.ToInt32(_ramCounter.NextValue());
            // Узнаем, когда мы сняли значение метрики
            var time =
            TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            // Теперь можно записать что-то посредством репозитория
            _ramMetricsRepository.Create(new Models.RamMetric
            {
                Time = (long)time.TotalSeconds,
                Value = (int)ramUsageInPercents
            });

            return Task.CompletedTask;
        }
    }
}
