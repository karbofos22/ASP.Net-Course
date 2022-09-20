using Microsoft.AspNetCore.Mvc;
using WeatherTest.Models;

namespace WeatherTest.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class MyWeatherController : ControllerBase
    {
        private readonly WeatherForecastHolder _holder;

        public MyWeatherController(WeatherForecastHolder holder)
        {
            _holder = holder;
        }

        [HttpPost("Add weather measurements")]
        public IActionResult Add([FromQuery] DateTime date, [FromQuery] int temperatureC)
        {
            _holder.Add(date, temperatureC);
            return Ok();
        }
        [HttpPut("Update weather measurements")]
        public IActionResult Update([FromQuery] DateTime date, [FromQuery] int temperatureC)
        {
            _holder.Update(date, temperatureC);
            return Ok();
        }
        [HttpDelete("Delete weather measurements by date")]
        public IActionResult Delete([FromQuery] DateTime date)
        {
            _holder.Delete(date);
            return Ok();
        }
        [HttpDelete("Fully delete weather record by date")]
        public IActionResult FullyDeleteRecordByDate([FromQuery] DateTime date)
        {
            _holder.FullyDeleteRecordByDate(date);
            return Ok();
        }
        [HttpGet("Get weather measurements by date")]
        public IActionResult Get([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            return Ok(_holder.Get(dateFrom, dateTo));
        }
    }
}
