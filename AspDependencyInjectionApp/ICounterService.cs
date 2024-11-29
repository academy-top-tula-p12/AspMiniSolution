namespace AspDependencyInjectionApp
{
    public interface ICounterService
    {
        int Count { get; }
    }

    public class RandomCounter : ICounterService
    {
        int count;

        Random random = new Random();

        public RandomCounter()
        {
            count = random.Next(1, 1000);
        }

        public int Count
        {
            get => count;
        }
    }

    public class CounterService
    {
        public RandomCounter Counter { get; }
        public CounterService(RandomCounter counter)
        {
            Counter = counter;
        }
    }

    public class CounterMiddleware
    {
        RequestDelegate next;
        int requestCount = 0;

        public CounterMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, RandomCounter randomCounter, CounterService counterService)
        {
            requestCount++;
            await context.Response.WriteAsync($"Requesrt #{requestCount}, random counter value: {randomCounter.Count}, counter service value: {counterService.Counter.Count}");
        }
    }

}
