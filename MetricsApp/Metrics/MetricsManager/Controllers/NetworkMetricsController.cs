using MetricsManager.Services.Client;
using MetricsManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MetricsManager.Models.Requests;

namespace MetricsManager.Controllers
{
    [Route("api/Network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        #region Services

        private IHttpClientFactory _httpClientFactory;
        private IAgentsRepository _agentsRepository;

        private IMetricsAgentClient _metricsAgentClient;

        #endregion

        #region Constructor

        public NetworkMetricsController(IMetricsAgentClient metricsAgentClient, IHttpClientFactory httpClientFactory, IAgentsRepository agentsRepository)
        {
            _httpClientFactory = httpClientFactory;
            _agentsRepository = agentsRepository;
            _metricsAgentClient = metricsAgentClient;
        }

        #endregion

        [HttpGet("getAll-by-id")]
        public ActionResult<NetworkMetricsResponse> GetMetricsFromAgent(
            [FromQuery] int agentId, [FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            return Ok(_metricsAgentClient.GetNetworkMetrics(new NetworkMetricRequest
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
