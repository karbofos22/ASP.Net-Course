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
    [Route("api/metrics/CPU")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private readonly IMapper _mapper;

        public CpuMetricsController(ICpuMetricsRepository cpuMetricRepository,
            ILogger<CpuMetricsController> logger, IMapper mapper)
        {
            _cpuMetricsRepository = cpuMetricRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // Уже не надо, почикать везде

        //[HttpPost("create")]
        //public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        //{
        //    _logger.LogInformation("Create cpu metric.");
        //    _cpuMetricsRepository.Create(_mapper.Map<CpuMetric>(request));
        //    return Ok();
        //}

        [HttpGet("All")]
        public ActionResult<IList<CpuMetricDto>> GetCpuMetricsAll() => 
            Ok(_mapper.Map<List<CpuMetricDto>>(_cpuMetricsRepository.GetAll()));


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<CpuMetricDto>> GetCpuMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get cpu metrics call.");
            return Ok(_mapper.Map<List<CpuMetricDto>>(_cpuMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }
    }
}

