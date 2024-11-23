using Microsoft.Extensions.FileProviders;

namespace AspMiniWelcomeApp
{
    static class Examples
    {
        public static async Task WelcomExample(WebApplication app)
        {
            //app.MapGet("/", () => "<h1>Hello World!</h1>");

            //app.UseWelcomePage();
            //app.Run(async (HttpContext context)
            //    => await context.Response.WriteAsync("Response with welcome!"));
            //app.Run(HandleRequest);
        }

        public static async Task RunExample(WebApplication app)
        {
            //int a = 10;

            //app.Run(async (context) => 
            //{ 
            //    a = a * 2;
            //    var response = context.Response;
            //    response.Headers.ContentLanguage = "ru-RU";
            //    response.Headers.ContentType = "text/html; charset=utf-8";
            //    response.Headers.Append("MyHeader", "Max Efimov");
            //    response.StatusCode = 404;

            //    await context.Response.WriteAsync($"<h1>Current a = {a}</h1>");
            //});
        }

        public static async Task RequestResponseExample(WebApplication app)
        {
            //app.Run(async (context) => 
            //{ 
            //    var response = context.Response;
            //    response.Headers.ContentLanguage = "ru-RU";
            //    response.Headers.ContentType = "text/html; charset=utf-8";
            //    response.Headers.Append("MyHeader", "Max Efimov");
            //    response.StatusCode = 404;
            //});

            app.Run(async (context) =>
            {
                string responseString = "";

                HttpRequest request = context.Request;
                foreach (var header in request.Headers)
                    responseString += $"{header.Key} => {header.Value}\n";

                responseString += $"\r\nPath => {request.Path}\n";

                responseString += $"\r\nQuery string => {request.QueryString}\n\n";

                foreach (var queryItem in request.Query)
                    responseString += $"Query key {queryItem.Key} => {queryItem.Value}\n";

                await context.Response.WriteAsync(responseString);


            });
        }

        public static async Task FileSendExample(WebApplication app)
        {
            app.Run(async context =>
            {
                //await context.Response.SendFileAsync(@"ada.jpg");
                //context.Response.ContentType = "text/html; charset=utf-8";
                //await context.Response.SendFileAsync(@"example.html");

                //context.Response.Headers.ContentDisposition = "attachment; filename=ada_picture.jpg";
                //await context.Response.SendFileAsync(@"ada.jpg");


                var provider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
                var fileInfo = provider.GetFileInfo("ada.jpg");

                context.Response.Headers.ContentDisposition = "attachment; filename=ada_picture.jpg";
                await context.Response.SendFileAsync(fileInfo);
            });
        }

        public static async Task FormExample(WebApplication app)
        {
            app.Run(async context =>
            {
                var response = context.Response;
                var request = context.Request;

                response.ContentType = "text/html; charset=utf-8";

                if (request.Path == "/employee")
                {
                    var form = request.Form;
                    string name = form["user_name"];
                    int age = Int32.Parse(form["user_age"].ToString());
                    string[] skils = form["user_scills"];

                    string responseString = $"<h3>Name {name}</h3><h3>Age {age}</h3><h4>Skils:</h4><ul>";
                    foreach (var skil in skils)
                        responseString += $"<li>{skil}</li>";
                    responseString += "</ul>";

                    await response.WriteAsync(responseString);

                }
                else
                {
                    await response.SendFileAsync("html/index.html");
                }
            });
        }

        public static async Task RedirectExample(WebApplication app)
        {
            app.Run(async context =>
            {
                var path = context.Request.Path;
                if (path == "/oldpage")
                {
                    //await context.Response.WriteAsync("Old page");
                    context.Response.Redirect("/newpage");
                }
                else if (path == "/newpage")
                    await context.Response.WriteAsync("New page");
                else
                    await context.Response.WriteAsync("Home page");
            });
        }


        public static async Task HandleRequest(HttpContext context)
        {
            await context.Response.WriteAsync("Response with welcome!");
        }

        
    }
}
