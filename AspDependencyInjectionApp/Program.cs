var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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


app.Run();
