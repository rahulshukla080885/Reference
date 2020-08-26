namespace HCEHB.ScoreService.Web.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Serilog;
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LogActionTimingStatisticsAttribute : ActionFilterAttribute
    {
        private DateTime _dateTime;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _dateTime = DateTime.Now;
            Log.Information($"Request starting HTTP {context.HttpContext.Request.Method} {context.HttpContext.Request.Path}");
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var timeTakenInSeconds = DateTime.Now.Subtract(_dateTime).TotalSeconds;
            Log.Information($"Request finished HTTP {context.HttpContext.Request.Method} {context.HttpContext.Request.Path} in {timeTakenInSeconds} seconds with status code {context.HttpContext.Response.StatusCode}");
            base.OnActionExecuted(context);
        }
    }
}
