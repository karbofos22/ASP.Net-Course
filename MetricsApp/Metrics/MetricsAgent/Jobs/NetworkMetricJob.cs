using MetricsAgent.Services;
using MetricsAgent.Services.Implementations;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _networkMetricsRepository;
        const string networkCard = "Broadcom NetLink [TM] Gigabit Ethernet";

        public NetworkMetricJob(INetworkMetricsRepository networkMetricsRepository)
        {
            _networkMetricsRepository = networkMetricsRepository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            // Получаем значение занятости CPU
            var networkUsageInPercents = GetNetworkUtilization(networkCard);
            // Узнаем, когда мы сняли значение метрики
            var time =
            TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            // Теперь можно записать что-то посредством репозитория
            _networkMetricsRepository.Create(new Models.NetworkMetric
            {
                Time = (long)time.TotalSeconds,
                Value = (int)networkUsageInPercents
            });

            return Task.CompletedTask;
        }
        private double GetNetworkUtilization(string networkCard)
        {
            const int numberOfIterations = 10;

            PerformanceCounter bandwidthCounter = new PerformanceCounter("Network Interface", "Current Bandwidth", networkCard);
            float bandwidth = bandwidthCounter.NextValue();

            PerformanceCounter dataSentCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", networkCard);

            PerformanceCounter dataReceivedCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", networkCard);

            float sendSum = 0;
            float receiveSum = 0;

            for (int index = 0; index < numberOfIterations; index++)
            {
                sendSum += dataSentCounter.NextValue();
                receiveSum += dataReceivedCounter.NextValue();
            }
            float dataSent = sendSum;
            float dataReceived = receiveSum;

            double utilization = (8 * (dataSent + dataReceived)) / (bandwidth * numberOfIterations) * 100;
            return utilization;
        }
    }
}
