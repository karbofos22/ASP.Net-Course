using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsRepository _hddMetricsRepository;
        private PerformanceCounter _hddCounter;

        public HddMetricJob(IHddMetricsRepository hddMetricsRepository)
        {
            _hddMetricsRepository = hddMetricsRepository;
            _hddCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }
        public Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine($"{DateTime.Now} hddmetrics job");

            // Получаем значение занятости CPU
            var hddUsageInPercents = Convert.ToInt32(_hddCounter.NextValue());
            // Узнаем, когда мы сняли значение метрики
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            // Теперь можно записать что-то посредством репозитория
            _hddMetricsRepository.Create(new Models.HddMetric
            {
                Time = (long)time.TotalSeconds,
                Value = (int)hddUsageInPercents
            });

            return Task.CompletedTask;
        }
    }
}
