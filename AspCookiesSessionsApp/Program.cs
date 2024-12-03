using AspCookiesSessionsApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(option =>
{
    option.Cookie.Name = "MyShop.Session";
    option.IdleTimeout = TimeSpan.FromMinutes(60);
});

Product? product = null; // = new()
//{
//    Id = 267,
//    Name = "Notebook",
//    Price = 120000,
//    Count = 1
//};

var app = builder.Build();
app.UseSession();

app.Run(async context =>
{
    if (context.Session.Keys.Contains("product"))
    {
        //await context.Response
        //             .WriteAsync($"Basket info: {context.Session.GetString("product")}");

        product = context.Session.GetData<Product>("product");
        await context.Response.WriteAsJsonAsync(product);
    }
    else
    {
        //context.Session.SetString("product", product.ToString());
        product = new()
        {
            Id = 267,
            Name = "Notebook",
            Price = 120000,
            Count = 1
        };
        context.Session.SetData<Product>("product", product);
        await context.Response.WriteAsync("Welcome to our shop");
    }

});


app.Run();



