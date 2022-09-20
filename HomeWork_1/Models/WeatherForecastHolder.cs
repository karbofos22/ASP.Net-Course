using Microsoft.AspNetCore.Mvc;

namespace WeatherTest.Models
{
    public class WeatherForecastHolder
    {
        private List<WeatherForecast> _values;

        public WeatherForecastHolder()
        {
            _values = new List<WeatherForecast>();
        }

        /// <summary>
        /// Добавить новое измерение температуры
        /// </summary>
        /// <param name="date">Дата фиксации температуры</param>
        /// <param name="temperatureC">Температура</param>
        public void Add(DateTime date, int temperatureC)
        {
            _values.Add(new WeatherForecast() { Date = date, TemperatureC = temperatureC });
        }
        /// <summary>
        /// Обновить существующее измерение температуры
        /// </summary>
        /// <param name="date">Дата фиксации температуры</param>
        /// <param name="temperatureC">Температура</param>
        public bool Update(DateTime date, int temperatureC)
        {
            foreach (var item in _values)
            {
                if(item.Date == date)
                {
                    item.TemperatureC = temperatureC;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Получить показатели температуры за временной период
        /// </summary>
        /// <param name="dateFrom">Начальная дата</param>
        /// <param name="dateTo">Конечная дата</param>
        /// <returns>Коллекция показателей температуры</returns>
        public List<WeatherForecast> Get(DateTime dateFrom, DateTime dateTo)
        {
            return _values.FindAll(item => item.Date >= dateFrom && item.Date <= dateTo);
        }
        /// <summary>
        /// Удалить показатель температуты на дату
        /// </summary>
        /// <param name="date">Дата фиксации показателя температуры</param>
        /// <returns>Результат выполнения операции</returns>
        public bool Delete(DateTime date)
        {
            foreach (var item in _values)
            {
                if (item.Date == date)
                {
                    item.TemperatureC = 0;
                    return true;
                }
            }
            return false;
        }
        public bool FullyDeleteRecordByDate(DateTime date)
        {
            foreach (var item in _values)
            {
                if (item.Date == date)
                {
                    _values.Remove(item);
                    return true;
                }
            }
            return false;
        }
    }
}
