using MetricsAgent.Models;
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

        public CpuMetricsController(ICpuMetricsRepository cpuMetricRepository,
            ILogger<CpuMetricsController> logger)
        {
            _cpuMetricsRepository = cpuMetricRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogInformation("Create cpu metric.");
            _cpuMetricsRepository.Create(new Models.CpuMetric
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        //[Route("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<CpuMetric>> GetCpuMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get cpu metrics call.");
            return Ok(_cpuMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }

    }

    public class MyType
    {
        public int Prop1 { get; set; }
        public int Prop2 { get; set; }
    }
}

