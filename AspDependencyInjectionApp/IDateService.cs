namespace AspDependencyInjectionApp
{
    public interface IDateService
    {
        string GetDate();
    }

    public class ShortDateService : IDateService
    {
        public string GetDate()
        {
            return DateTime.Now.ToShortTimeString();
        }
    }

    public class LongDateService : IDateService
    {
        public string GetDate()
        {
            return DateTime.Now.ToLongDateString();
        }
    }

    public static class DateServiceProviderExtensions
    {
        public static void AddShortDateService(this IServiceCollection services)
        {
            services.AddTransient<ShortDateService>();
        }

        public static void AddLongDateService(this IServiceCollection services)
        {
            services.AddTransient<LongDateService>();
        }

        public static void AddLongTimeService(this IServiceCollection services)
        {
            services.AddTransient<LongTimeService>();
        }
    }







    public class LongTimeService
    {
        public string GetTime()
        {
            return DateTime.Now.ToLongTimeString();
        }
    }

}
