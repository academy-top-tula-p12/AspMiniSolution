namespace AspDependencyInjectionApp
{
    public static class Examples
    {
        public static void DefaultServicesViewExamples(WebApplicationBuilder builder, WebApplication app)
        {
            var services = builder.Services;

            app.Run(async context =>
            {
                string responseString = "<table><tr><td>Name</td><td>LifeTime</td><td>Implemetation</td></tr>";
                foreach (var service in services)
                {
                    responseString += "<tr>";
                    responseString += $"<td>{service.ServiceType.Name}</td>";
                    responseString += $"<td>{service.Lifetime}</td>";
                    responseString += $"<td>{service.ImplementationType?.Name}</td>";
                    responseString += "</tr>";
                }
                responseString += "</table>";

                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync(responseString);
            });
        }
    
        public static void CreateUserServiceExample(WebApplicationBuilder builder, WebApplication app)
        {
            string responseString = "";

            app.Use(async (context, next) =>
            {
                //var dateService = app.Services.GetService<IDateService>();
                //var timeService = app.Services.GetService<LongTimeService>();

                var dateService = context.RequestServices.GetService<IDateService>();
                var timeService = context.RequestServices.GetRequiredService<LongTimeService>();

                responseString += $"Current date: {dateService?.GetDate()}, current time: {timeService?.GetTime()}\n";

                Thread.Sleep(3000);

                next?.Invoke(context);

                //await context.Response.WriteAsync($"Current date: {dateService?.GetDate()}, current time: {timeService?.GetTime()}");
            });

            app.Run(async context =>
            {

                var timeService = app.Services.GetService<LongTimeService>();
                responseString += $"Second current time: {timeService?.GetTime()}";

                context.Response.ContentType = "text/plain; charset=utf-8";
                await context.Response.WriteAsync(responseString);
            });
        }
    }
}
