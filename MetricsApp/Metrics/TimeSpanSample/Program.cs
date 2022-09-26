namespace TimeSpanSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TimeSpan timeStart = new(3000);

            timeStart = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            Console.WriteLine(timeStart);

            Random random = new();

            Thread.Sleep(random.Next(1500, 5000));

            TimeSpan timeEnd = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            Console.WriteLine(timeEnd);

            TimeSpan timeDelta = timeStart - timeEnd;

            Console.WriteLine(timeDelta);
        }
    }
}