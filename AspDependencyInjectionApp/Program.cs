using AspDependencyInjectionApp;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddTransient<IDateService, LongDateService>();
//builder.Services.AddTransient<LongTimeService>();

//builder.Services.AddLongDateService();
//builder.Services.AddLongTimeService();


//builder.Services.AddTransient<RandomCounter>();
//builder.Services.AddTransient<CounterService>();

//builder.Services.AddScoped<RandomCounter>();
//builder.Services.AddScoped<CounterService>();

//builder.Services.AddSingleton<RandomCounter>();
//builder.Services.AddSingleton<CounterService>();



//builder.Services.AddTransient<IMessageLogger, JsonMessageLogger>();
//builder.Services.AddTransient<IMessageLogger, XmlMessageLogger>();

var messageService = new MessageService();

builder.Services.AddSingleton<IMessageLogger>(messageService);
builder.Services.AddSingleton<IMessageSender>(messageService);

var app = builder.Build();

//app.UseMiddleware<CounterMiddleware>();
//app.UseMiddleware<MessageLoggerMiddleware>();

app.UseMiddleware<XmlLoggerMiddleware>();
app.UseMiddleware<MailSenderMiddleware>();

app.Run();





