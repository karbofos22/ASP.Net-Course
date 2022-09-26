using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTests
{
    public class DotnetMetricsControllerTests
    {
        private DotnetMetricsController _dotnetMetricsController;

        public DotnetMetricsControllerTests()
        {
            _dotnetMetricsController = new DotnetMetricsController();
        }

        [Fact]
        public void GetDotnetMetrics_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            var result = _dotnetMetricsController.GetDotnetMetrics(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
