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
    [Route("api/metrics/Ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _ramMetricsRepository;
        private readonly IMapper _mapper;

        public RamMetricsController(IRamMetricsRepository ramMetricRepository,
            ILogger<RamMetricsController> logger, IMapper mapper)
        {
            _ramMetricsRepository = ramMetricRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _logger.LogInformation("Create ram metric.");
            _ramMetricsRepository.Create(_mapper.Map<RamMetric>(request));
            return Ok();
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<RamMetricDto>> GetRamMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get ram metrics call.");
            return Ok(_mapper.Map<List<RamMetricDto>>(_ramMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }
    }
}
