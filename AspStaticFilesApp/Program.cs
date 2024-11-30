var builder = WebApplication.CreateBuilder(
    new WebApplicationOptions() { WebRootPath = "html"}
    );
var app = builder.Build();


DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
defaultFilesOptions.DefaultFileNames.Clear();
defaultFilesOptions.DefaultFileNames.Add("hello.html");

app.UseDefaultFiles(defaultFilesOptions);
//app.UseStaticFiles();
app.UseFileServer(new FileServerOptions
{
        
});

app.Run(async context => await context.Response.WriteAsync("Response"));

app.Run();
