var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.Map("/", () => "Home Page");
//app.Map("/about", () => "About Page");
//app.Map("/contact", async (context) => { await context.Response.WriteAsync("Contact Page"); });

//app.Map("/view", (IEnumerable<EndpointDataSource> endPoints) =>
//{
//    return string.Join("\n", endPoints.SelectMany(i => i.Endpoints));
//});

app.Map("/news/{category?}", (string? category = "all") =>
{
    return $"News list at category {category}";
});

app.Map("/flight/{airport}/{name}", (string airport, string name) => $"Airport: {airport}, flight name: {name}");

app.Run();
