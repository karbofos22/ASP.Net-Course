using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/Network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _networkMetricsRepository;
        private readonly IMapper _mapper;

        public NetworkMetricsController(INetworkMetricsRepository networkMetricRepository,
            ILogger<NetworkMetricsController> logger, IMapper mapper)
        {
            _networkMetricsRepository = networkMetricRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public ActionResult<IList<NetworkMetricDto>> GetNetworkMetricsAll() =>
            Ok(_mapper.Map<List<NetworkMetricDto>>(_networkMetricsRepository.GetAll()));

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<NetworkMetricDto>> GetNetworkMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get network metrics call.");
            return Ok(_mapper.Map<List<NetworkMetricDto>>(_networkMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }
    }
}
