namespace AspCookiesSessionsApp
{
    static public class Examples
    {
        public static void ContextItemsExample(WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                context.Items.Add("message", "Hello world");
                await next.Invoke(context);
            });

            //app.MapGet("/", () => "Hello World!");

            app.Run(async conext =>
            {
                if (conext.Items.ContainsKey("message"))
                    await conext.Response.WriteAsync($"Message: {conext.Items["message"]}");
                else
                    await conext.Response.WriteAsync("Not message");
            });
        }

        public static void CookiesExample(WebApplication app)
        {
            app.Run(async context =>
            {
                if (context.Request.Cookies.ContainsKey("login"))
                {
                    string? loginValue = context.Request.Cookies["login"];
                    await context.Response.WriteAsync($"Login value: {loginValue}");
                }
                else
                {
                    context.Response.Cookies.Append("login", "master");
                    await context.Response.WriteAsync("Welcome to our site!");
                }
            });

        }
    }
}
