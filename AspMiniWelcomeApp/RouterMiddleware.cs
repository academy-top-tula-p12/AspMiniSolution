namespace AspMiniWelcomeApp
{
    public class MyRouterMiddleware
    {
        public MyRouterMiddleware(RequestDelegate _){}

        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path;

            switch (path)
            {
                case "/index":
                    await context.Response.WriteAsync("Home page");
                    break;
                case "/about":
                    await context.Response.WriteAsync("About page");
                    break;
                case "/contact":
                    await context.Response.WriteAsync("Contact page");
                    break;
                default:
                    context.Response.StatusCode = 404;
                    break;
            }
        }
    }

    public class AuthMiddleware
    {
        RequestDelegate next;
        public AuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string password = context.Request.Query["password"];
            if (password != "qwerty")
                context.Response.StatusCode = 403;
            else
                await next.Invoke(context);
        }
    }

    public class ErrorHandlerMiddlware
    {
        RequestDelegate next;
        public ErrorHandlerMiddlware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await next.Invoke(context);

            var code = context.Response.StatusCode;
            if (code == 404)
                await context.Response.WriteAsync("Page not found");
            if (code == 403)
                await context.Response.WriteAsync("User not auth");
        }
    }

}
