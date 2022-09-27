using MetricsAgent.Models;
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

        public DotnetMetricsController(IDotnetMetricsRepository dotnetMetricRepository,
            ILogger<DotnetMetricsController> logger)
        {
            _dotnetMetricsRepository = dotnetMetricRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotnetMetricCreateRequest request)
        {
            _logger.LogInformation("Create dotnet metric.");
            _dotnetMetricsRepository.Create(new Models.DotnetMetric
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        //[Route("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<DotnetMetric>> GetDotnetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get dotnet metrics call.");
            return Ok(_dotnetMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }
    }
}
