using AspMiniWelcomeApp;
using Microsoft.Extensions.FileProviders;

var options = new WebApplicationOptions() { Args = args };
var builder = WebApplication.CreateBuilder(options);
var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddlware>();
app.UseMiddleware<AuthMiddleware>();
app.UseMiddleware<MyRouterMiddleware>();


app.Run();







