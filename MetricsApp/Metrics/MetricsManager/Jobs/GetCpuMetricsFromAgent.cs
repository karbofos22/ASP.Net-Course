using MetricsManager.Controllers;
using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Services;
using MetricsManager.Services.Client;
using Quartz;

namespace MetricsManager.Jobs
{
    public class GetCpuMetricsFromAgent : IJob
    {
        private IMetricsAgentClient _metricsAgentClient;
        private IAgentsRepository _agentsRepository;
        private CpuMetricsController _cpuMetricsController;

        public GetCpuMetricsFromAgent(IAgentsRepository agentsRepository, IMetricsAgentClient metricsAgentClient, CpuMetricsController cpuMetricsController)
        {
            _metricsAgentClient = metricsAgentClient;
            _agentsRepository = agentsRepository;
            _cpuMetricsController = cpuMetricsController; 
        }

        public Task Execute(IJobExecutionContext context)
        {
           //var data1 = _cpuMetricsController.GetMetricsFromAgent()

            //Debug.WriteLine($"{DateTime.Now} cpumetrics job");
            //_agentsRepository.Create(new Models.CpuMetric
            //{
            //    Time = (long)time.TotalSeconds,
            //    Value = (int)cpuUsageInPercents
            //});

            return Task.CompletedTask;
        }
    }
}
