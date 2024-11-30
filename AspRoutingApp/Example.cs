namespace AspRoutingApp
{
    public class DateService
    {
        public string Date => DateTime.Now.ToLongDateString();
    }

    static public class Example
    {
        static public void RouteWelcomeExample(WebApplication app)
        {
            //app.Map("/", () => "Home Page");
            //app.Map("/about", () => "About Page");
            //app.Map("/contact", async (context) => { await context.Response.WriteAsync("Contact Page"); });

            //app.Map("/view", (IEnumerable<EndpointDataSource> endPoints) =>
            //{
            //    return string.Join("\n", endPoints.SelectMany(i => i.Endpoints));
            //});

            //app.Map("/news/{category?}", (string? category = "all") =>
            //{
            //    return $"News list at category {category}";
            //});

            //app.Map("/flight/{airport}/{name}", (string airport, string name) => $"Airport: {airport}, flight name: {name}");

            //app.Map("/user/{id:regex(^\\d{{2}}-\\d{{2}}$)}", (string id) => $"user id: {id}");

            //app.Map("/date", (DateService service) => service.Date);
            app.Map("/date", GetCurrenDate);


            app.Run();

            /*
            Path: /controller/action/params/../../..

            */
        }

        static string GetCurrenDate(DateService service)
        {
            return $"Current date: {service.Date}";
        }
    }
}
