using Microsoft.AspNetCore.Mvc.Filters;

namespace Dotnet.OriginAssignment.Api.Filter
{
    public class TimingActionFilterAttribute : ActionFilterAttribute
    {
        private const string TimingHeaderName = "X-Request-Time";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items["RequestStartTime"] = DateTime.UtcNow;

            var path = context.HttpContext.Request.Path;

            var headers = context.HttpContext.Request.Headers
                .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            var queryString = context.HttpContext.Request.QueryString;

            var requestBody = "";
            if (context.HttpContext.Request.ContentLength.HasValue && context.HttpContext.Request.ContentLength > 0)
            {
                using (var reader = new StreamReader(context.HttpContext.Request.Body))
                {
                    requestBody = reader.ReadToEndAsync().Result;
                }

            }

            base.OnActionExecuting(context);
        }


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Items.TryGetValue("RequestStartTime", out var startTimeObj) &&
                startTimeObj is DateTime startTime)
            {
                var endTime = DateTime.UtcNow;
                var elapsedTime = endTime - startTime;
                context.HttpContext.Response.Headers.Add(TimingHeaderName, elapsedTime.TotalSeconds.ToString());
            }

            base.OnActionExecuted(context);
        }
    }
}
