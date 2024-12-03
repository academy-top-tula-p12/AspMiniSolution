using AspConfigurationApp;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration
//       .AddInMemoryCollection(
//        new Dictionary<string, string>
//        {
//            { "login", "admin" },
//            { "password", "qwerty" }
//        });

//builder.Configuration
//       .AddJsonFile("config.json")
//       .AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile("config.json");
UserConfig userConfig = new();
builder.Configuration
       .Bind(userConfig);

//builder.Configuration
//       //.AddXmlFile("config.xml")
//       .AddXmlFile("appsettings.xml");

//builder.Configuration
//       .AddIniFile("config.ini");


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




//app.Map("/", (IConfiguration appConfig) => $"{appConfig["login"]} - {appConfig["password"]}");
//app.Map("/", (IConfiguration appConfig) => $"{appConfig["Employee:Name:First"]}");

app.Run(async context => await context.Response
                                      .WriteAsync(userConfig.ToString()));
                                      //.WriteAsJsonAsync(userConfig));

app.Run();
