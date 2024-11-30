using System.Reflection.Metadata.Ecma335;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddTransient<DateService>();

var app = builder.Build();


app.Use(async (context, next) =>
{
    Console.WriteLine("first middleware start");
    await next.Invoke(context);
    Console.WriteLine("first middleware finish");
});

app.Map("/", () =>
{
    Console.WriteLine("Index endpoint");
    return "Index Page";
});

app.Use(async (context, next) =>
{
    Console.WriteLine("second middleware start");
    await next.Invoke(context);
    Console.WriteLine("second middleware finish");
});

app.Map("/end", () =>
{
    Console.WriteLine("End endpoint");
    return "End Page";
});

app.Run();

