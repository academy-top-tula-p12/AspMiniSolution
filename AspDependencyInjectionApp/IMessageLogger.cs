namespace AspDependencyInjectionApp
{
    public interface IMessageLogger
    {
        string Type { get; }
        string Log(string message);
    }

    public class JsonMessageLogger : IMessageLogger
    {
        public string Type
        {
            get => "json";
        }
        public string Log(string message)
        {
            return "{" + $"\"message\": \"{message}\"" + "}"; 
        }
    }

    public class XmlMessageLogger : IMessageLogger
    {
        public string Type
        {
            get => "xml";
        }
        public string Log(string message)
        {
            return $"<message>{message}</message>";
        }
    }

    public class MessageLoggerMiddleware
    {
        RequestDelegate next;
        IEnumerable<IMessageLogger> messageLoggers;

        public MessageLoggerMiddleware(RequestDelegate next, 
                                       IEnumerable<IMessageLogger> messageLoggers)
        {
            this.next = next;
            this.messageLoggers = messageLoggers;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.ContentType = "text/plain; charset=utf-8";
            string message = "Hello world";
            string responseString = "";

            foreach (var logService in messageLoggers)
                if(logService.Type == "json")
                    responseString += logService.Log(message) + "\n";

            await context.Response.WriteAsync(responseString);
        }
    }


    public interface IMessageSender
    {
        string Send(string message);
    }

    public class MessageService : IMessageLogger, IMessageSender
    {
        public string Type => "xml";
        Random rand = new Random();
        string randMessage;
        public MessageService()
        {
            randMessage = rand.Next(1000, 2000).ToString();
        }


        public string Log(string message)
        {
            return $"<message>{randMessage}</message>";
        }

        public string Send(string message)
        {
            return $"mailto: {randMessage}";
        }
    }

    public class XmlLoggerMiddleware
    {
        RequestDelegate next;
        IMessageLogger messageLogger;
        string message;

        public XmlLoggerMiddleware(RequestDelegate next,
                                IMessageLogger messageLogger,
                                string message = "Hello world")
        {
            this.next = next;
            this.messageLogger = messageLogger;
            this.message = message;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/xml")
                await context.Response.WriteAsync($"{messageLogger.Log(message)}");
            else
                await next.Invoke(context);
        }

    }

    public class MailSenderMiddleware
    {
        RequestDelegate next;
        IMessageSender messageSender;
        string message;

        public MailSenderMiddleware(RequestDelegate next,
                                IMessageSender messageSender,
                                string message = "Hello world")
        {
            this.next = next;
            this.messageSender = messageSender;
            this.message = message;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/view")
                await context.Response.WriteAsync($"{messageSender.Send(message)}");
            else
                await next.Invoke(context);
        }

    }

}
