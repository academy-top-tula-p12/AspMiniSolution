using AspMiniWelcomeApp;
using Microsoft.Extensions.FileProviders;

var options = new WebApplicationOptions() { Args = args };
var builder = WebApplication.CreateBuilder(options);
var app = builder.Build();

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

    if(request.Path == "/flight")
    {
        Flight? flightFromClient = await request.ReadFromJsonAsync<Flight>();
        if(flightFromClient != null)
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



app.Run();




