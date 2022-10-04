using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class DontnetMetricJob : IJob
    {
        private readonly IDotnetMetricsRepository _dotnetMetricsRepository;
        private PerformanceCounter _dotnetCounter;

        public DontnetMetricJob(IDotnetMetricsRepository dotnetMetricsRepository)
        {
            _dotnetMetricsRepository = dotnetMetricsRepository;
            _dotnetCounter = new PerformanceCounter(".NET CLR Exceptions", "# of Exceps Thrown / sec", "_Global_");
        }
        public Task Execute(IJobExecutionContext context)
        {
            //Debug.WriteLine($"{DateTime.Now} cpumetrics job");

            // Получаем значение занятости CPU
            var dotnetUsageInPercents = Convert.ToInt32(_dotnetCounter.NextValue());
            // Узнаем, когда мы сняли значение метрики
            var time =
            TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            // Теперь можно записать что-то посредством репозитория
            _dotnetMetricsRepository.Create(new Models.DotnetMetric
            {
                Time = (long)time.TotalSeconds,
                Value = (int)dotnetUsageInPercents
            });

            return Task.CompletedTask;
        }
    }
}
