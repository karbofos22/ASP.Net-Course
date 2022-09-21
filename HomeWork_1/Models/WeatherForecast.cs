namespace WeatherTest.Models
{
    /// <summary>
    ///Прогноз погоды Forecast
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        ///Дата измерения
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Температура(по Цельсию)
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Температура(по Фаренгейту)
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.556);

        /// <summary>
        /// Описание погоды
        /// </summary>
        public string Summary => TemperatureC switch
        {
            <= -30 => "Freezing",
            <= -20 => "Bracing",
            <= -10 => "Chilly",
            <= 5 => "Cool",
            <= 10 => "Mild",
            <= 20 => "Warm",
            <= 30 => "Balmy",
            <= 35 => "Hot",
            _ => "No case available"
        };
    }
}
