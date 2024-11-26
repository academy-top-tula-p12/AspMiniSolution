namespace AspMiniWelcomeApp
{
    public class QueryStringMiddleware
    {
        RequestDelegate next;
        string brand;

        public QueryStringMiddleware(RequestDelegate next, string brand)
        {
            this.next = next;
            this.brand = brand;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string queryKey = "brand";
            var queryValue  = context.Request.Query[queryKey];

            if(queryValue != brand.ToLower())
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync($"Incorrect value {queryValue} for token {queryKey}");
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
    
    public static class QueryStringExtension
    {
        public static IApplicationBuilder UseQueryString(this IApplicationBuilder builder, string brand)
        {
            return builder.UseMiddleware<QueryStringMiddleware>(brand);
        }
    }
}
