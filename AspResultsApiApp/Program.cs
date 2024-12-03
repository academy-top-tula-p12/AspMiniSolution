using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/", () => Results.Text("Hello world"));

app.Map("/content", () => Results.Content("<h1>Content Result</h1>", "text/html", Encoding.Unicode));

app.Map("/json", () => Results.Json(new { Name = "Bobby", Age = 28 }));

app.Map("/old", () => Results.Redirect("/content"));

app.Map("/error", () => Results.NotFound(new { Message = "User not found" }));

app.Map("/file", async () =>
{
    string file = "files/airbus.png";
    byte[] buffer = await File.ReadAllBytesAsync(file);

    return Results.File(buffer, "image/png", "img_airplan.png");
});

app.Run();
