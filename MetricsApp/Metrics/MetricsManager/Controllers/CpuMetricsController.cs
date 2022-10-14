using MetricsManager.Agents;
using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Services;
using MetricsManager.Services.Client;
using MetricsManager.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MetricsManager.Controllers
{
    [Route("api/Cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        #region Services

        private IHttpClientFactory _httpClientFactory;
        private IAgentsRepository _agentsRepository;

        private IMetricsAgentClient _metricsAgentClient;

        #endregion

        #region Constructor

        public CpuMetricsController(IMetricsAgentClient metricsAgentClient, IHttpClientFactory httpClientFactory, IAgentsRepository agentsRepository)
        {
            _httpClientFactory = httpClientFactory;
            _agentsRepository = agentsRepository;
            _metricsAgentClient = metricsAgentClient;
        }

        #endregion

        [HttpGet("getAll-by-id")]
        public ActionResult<CpuMetricsResponse> GetMetricsFromAgent(
            [FromQuery] int agentId, [FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            return Ok(_metricsAgentClient.GetCpuMetrics(new CpuMetricRequest
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            }));
        }



        [HttpGet("get-all")]
        public IActionResult GetMetricsFromAll(
            [FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
