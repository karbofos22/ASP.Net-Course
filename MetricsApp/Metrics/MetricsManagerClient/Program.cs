namespace MetricsManagerClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            AgentsClient agentClient = new AgentsClient("http://localhost:5088/", new HttpClient());
            CpuClient cpuMetricsClient = new CpuClient("http://localhost:5088/", new HttpClient());
            DotnetClient dotnetMetricsClient = new DotnetClient("http://localhost:5088/", new HttpClient());
            NetworkClient networkMetricsClient = new NetworkClient("http://localhost:5088/", new HttpClient());
            HddClient hddMetricsClient = new HddClient("http://localhost:5088/", new HttpClient());
            RamClient ramMetricsClient = new RamClient("http://localhost:5088/", new HttpClient());

            Console.WriteLine("Введите номер агента");
            int.TryParse(Console.ReadLine(), out int num);
            
            await agentClient.GetByIdAsync(num);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Задачи");
                Console.WriteLine("==============================================");
                Console.WriteLine("1 - Получить метрики за последнюю минуту (CPU)");
                Console.WriteLine("2 - Получить метрики за последнюю минуту (Dotnet)");
                Console.WriteLine("3 - Получить метрики за последнюю минуту (Network)");
                Console.WriteLine("4 - Получить метрики за последнюю минуту (Hdd)");
                Console.WriteLine("5 - Получить метрики за последнюю минуту (Ram)");
                Console.WriteLine("====");
                Console.WriteLine("0 - Завершение работы приложения");
                Console.WriteLine("==============================================");
                Console.Write("Введите номер задачи: ");
                if (int.TryParse(Console.ReadLine(), out int taskNumber))
                {
                    switch (taskNumber)
                    {
                        case 0:
                            Console.WriteLine("Завершение работы приложения.");
                            Console.ReadKey(true);
                            break;
                        case 1:
                            try
                            {

                                TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                                TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);

                                CpuMetricsResponse response = await cpuMetricsClient.GetAllByIdAsync(
                                    1,
                                    fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                    toTime.ToString("dd\\.hh\\:mm\\:ss"));

                                foreach (CpuMetric metric in response.Metrics)
                                {
                                    Console.WriteLine($"{TimeSpan.FromSeconds(metric.Time).ToString("dd\\.hh\\:mm\\:ss")} >>> {metric.Value}");
                                }
                                Console.WriteLine("Нажмите любую клавишу для продолжения работы ...");
                                Console.ReadKey(true);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Произошла ошибка при попыте получить CPU метрики.\n{e.Message}");
                            }

                            break;
                        case 2:
                            try
                            {

                                TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                                TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);

                                DotnetMetricsResponse response = await dotnetMetricsClient.GetAllByIdAsync(
                                    1,
                                    fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                    toTime.ToString("dd\\.hh\\:mm\\:ss"));

                                foreach (DotnetMetric metric in response.Metrics)
                                {
                                    Console.WriteLine($"{TimeSpan.FromSeconds(metric.Time).ToString("dd\\.hh\\:mm\\:ss")} >>> {metric.Value}");
                                }
                                Console.WriteLine("Нажмите любую клавишу для продолжения работы ...");
                                Console.ReadKey(true);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Произошла ошибка при попыте получить Dotnet метрики.\n{e.Message}");
                            }

                            break;
                        case 3:
                            try
                            {

                                TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                                TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);

                                NetworkMetricsResponse response = await networkMetricsClient.GetAllByIdAsync(
                                    1,
                                    fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                    toTime.ToString("dd\\.hh\\:mm\\:ss"));

                                foreach (NetworkMetric metric in response.Metrics)
                                {
                                    Console.WriteLine($"{TimeSpan.FromSeconds(metric.Time).ToString("dd\\.hh\\:mm\\:ss")} >>> {metric.Value}");
                                }
                                Console.WriteLine("Нажмите любую клавишу для продолжения работы ...");
                                Console.ReadKey(true);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Произошла ошибка при попыте получить Network метрики.\n{e.Message}");
                            }

                            break;
                        case 4:
                            try
                            {

                                TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                                TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);

                                HddMetricsResponse response = await hddMetricsClient.GetAllByIdAsync(
                                    1,
                                    fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                    toTime.ToString("dd\\.hh\\:mm\\:ss"));

                                foreach (HddMetric metric in response.Metrics)
                                {
                                    Console.WriteLine($"{TimeSpan.FromSeconds(metric.Time).ToString("dd\\.hh\\:mm\\:ss")} >>> {metric.Value}");
                                }
                                Console.WriteLine("Нажмите любую клавишу для продолжения работы ...");
                                Console.ReadKey(true);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Произошла ошибка при попыте получить HDD метрики.\n{e.Message}");
                            }

                            break;
                        case 5:
                            try
                            {

                                TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                                TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);

                                RamMetricsResponse response = await ramMetricsClient.GetAllByIdAsync(
                                    1,
                                    fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                    toTime.ToString("dd\\.hh\\:mm\\:ss"));

                                foreach (RamMetric metric in response.Metrics)
                                {
                                    Console.WriteLine($"{TimeSpan.FromSeconds(metric.Time).ToString("dd\\.hh\\:mm\\:ss")} >>> {metric.Value}");
                                }
                                Console.WriteLine("Нажмите любую клавишу для продолжения работы ...");
                                Console.ReadKey(true);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Произошла ошибка при попыте получить RAM метрики.\n{e.Message}");
                            }

                            break;
                        default:
                            Console.WriteLine("Введите корректный номер подзадачи.");
                            break;
                    }
                }
            }
        }
    }
}
