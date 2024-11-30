var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddInMemoryCollection(
    new Dictionary<string, string>
    {
        { "login", "admin" },
        { "password", "qwerty" }
    });

var app = builder.Build();

//app.Configuration["login"] = "admin";
//app.Configuration["password"] = "qwerty";


//app.Run(async context =>
//{
//    string responseString = "";
//    responseString += $"login: {app.Configuration["login"]} ";
//    responseString += $"pass: {app.Configuration["password"]}";

//    await context.Response.WriteAsync(responseString);
//});

app.Map("/", (IConfiguration appConfig) => $"{appConfig["login"]}");


app.Run();
