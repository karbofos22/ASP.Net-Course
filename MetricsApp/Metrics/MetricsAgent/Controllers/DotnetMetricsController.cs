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
    [Route("api/metrics/Dotnet/errors-count")]
    [ApiController]
    public class DotnetMetricsController : ControllerBase
    {
        private readonly ILogger<DotnetMetricsController> _logger;
        private readonly IDotnetMetricsRepository _dotnetMetricsRepository;
        private readonly IMapper _mapper;

        public DotnetMetricsController(IDotnetMetricsRepository dotnetMetricRepository,
            ILogger<DotnetMetricsController> logger, IMapper mapper)
        {
            _dotnetMetricsRepository = dotnetMetricRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public ActionResult<IList<DotnetMetricDto>> GetDotnetMetricsAll() =>
            Ok(_mapper.Map<List<DotnetMetricDto>>(_dotnetMetricsRepository.GetAll()));

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<DotnetMetricDto>> GetDotnetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get dotnet metrics call.");
            return Ok(_mapper.Map<List<DotnetMetricDto>>(_dotnetMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }
    }
}
