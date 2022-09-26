using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTests
{
    public class RamMerticsControllerTests
    {
        private RamMetricsController _ramMetricsController;

        public RamMerticsControllerTests()
        {
            _ramMetricsController = new RamMetricsController();
        }

        [Fact]
        public void GetCpuMetrics_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            var result = _ramMetricsController.GetRamMetrics(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
