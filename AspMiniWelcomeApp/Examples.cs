using Microsoft.AspNetCore.Builder;
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

        public static async Task JsonSendExample(WebApplication app)
        {
            Flight flight = new()
            {
                Id = 1,
                Name = "ASD-123",
                FromCity = "Moscow",
                ToCity = "Kazan",
                Date = new DateOnly(2024, 12, 20),
                Time = new TimeOnly(18, 30)
            };

            app.Run(async context =>
            {
                //await context.Response.WriteAsJsonAsync<Flight>(flight);

                var request = context.Request;
                var response = context.Response;

                if (request.Path == "/flight")
                {
                    Flight? flightFromClient = await request.ReadFromJsonAsync<Flight>();
                    if (flightFromClient != null)
                    {
                        await response.WriteAsync(flightFromClient.ToString());
                    }
                }
                else
                {
                    response.ContentType = "text/html; charset=utf-8";
                    await response.SendFileAsync("html/flight.html");
                }

            });
        }

        public static async Task FileDownloadExample(WebApplication app)
        {
            app.Run(async context =>
            {
                var response = context.Response;
                var request = context.Request;

                response.ContentType = "text/html; charset=utf-8";

                if (request.Path == "/download" && request.Method == "POST")
                {
                    IFormFileCollection formFiles = request.Form.Files;

                    var pathDirectory = $"{Directory.GetCurrentDirectory()}/downloads";
                    Directory.CreateDirectory(pathDirectory);

                    foreach (var file in formFiles)
                    {
                        string fileName = $"{pathDirectory}/{file.FileName}";
                        using (var stream = new FileStream(fileName, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                    await response.WriteAsync("Files saveded to server");
                }
                else
                    await response.SendFileAsync("html/fileform.html");
            });
        }

        public static async Task UseMapExample(WebApplication app)
        {
            //app.Use(async (context, next) =>
            //{
            //    // actions before called next
            //    await next.Invoke();
            //    // action after 
            //});

            string date = null!;
            string time = null!;

            string responseString = null!;

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Action at forward way");
            //    date = DateTime.Now.ToLongDateString();
            //    await next.Invoke(context);
            //    Console.WriteLine("Action at reverse way");
            //});

            //app.Use(MiddleWareTwo);

            app.UseWhen(
                context => context.Request.Path == "/date",
                builder =>
                {
                    builder.Use(async (context, next) =>
                    {
                        responseString = DateTime.Now.ToLongDateString();
                        await next.Invoke();
                    });
                });

            app.UseWhen(
                context => context.Request.Path == "/time",
                builder =>
                {
                    builder.Use(async (context, next) =>
                    {
                        responseString = DateTime.Now.ToLongTimeString();
                        await next.Invoke();
                    });
                });


            app.Map("/about",
                builder =>
                {
                    builder.Run(async context => context.Response.WriteAsync("About Page"));
                });

            app.Map("/gallery",
                builder =>
                {
                    builder.Run(async context => context.Response.WriteAsync("Gallery Page"));
                });


            app.Run(async context =>
            {
                context.Response.ContentType = "text/plain; charset=utf-8";
                await context.Response.WriteAsync($"{responseString}");
            });
        }

        public static async Task MapGroupsExample(WebApplication app)
        {
            string message = "";


            app.Map("/airlines", appBuilder =>
            {
                appBuilder.Map("/add", (builder) => SendString(builder, "Add airline"));
                appBuilder.Map("/edit", (builder) => SendString(builder, "Edit airline"));
                appBuilder.Map("", (builder) => SendString(builder, "View All airlines"));
            });

            app.Map("/airports", appBuilder =>
            {
                appBuilder.Map("/add", (builder) => SendString(builder, "Add airport"));
                appBuilder.Map("/edit", (builder) => SendString(builder, "Edit airport"));
                appBuilder.Map("", (builder) => SendString(builder, "View All airports"));
            });
        }

        public static async Task ClassMiddlewareExample(WebApplication app)
        {
            //app.UseMiddleware<QueryStringMiddleware>();

            string brand = "Avito";

            app.UseQueryString(brand);

            app.Run(async context => await context.Response.WriteAsync($"Welcome to {brand}!"));
        }

        public static async Task HandleRequest(HttpContext context)
        {
            await context.Response.WriteAsync("Response with welcome!");
        }

        public static async Task MiddleWareTwo(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Middleware2: Action at forward way");
            string time = DateTime.Now.ToShortTimeString();
            await next.Invoke(context);
            Console.WriteLine("Middleware2: Action at reverse way");
        }

        public static void SendString(IApplicationBuilder builder, string message)
        {
            builder.Run(async context => context.Response.WriteAsync(message));
        }

    }
}
