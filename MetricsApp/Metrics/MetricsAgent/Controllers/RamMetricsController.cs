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

        [HttpGet("All")]
        public ActionResult<IList<RamMetricDto>> GetRamMetricsAll() =>
            Ok(_mapper.Map<List<RamMetricDto>>(_ramMetricsRepository.GetAll()));

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<GetRamMetricsResponse> GetRamMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get ram metrics call.");
            return Ok(new GetRamMetricsResponse
            {
                Metrics = _ramMetricsRepository.GetByTimePeriod(fromTime, toTime)
                        .Select(metric => _mapper.Map<RamMetricDto>(metric)).ToList()
            });
        }
    }
}
