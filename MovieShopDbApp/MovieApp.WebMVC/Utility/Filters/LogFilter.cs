using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace MovieApp.WebMVC.Utility.Filters
{
    public class LogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var controllerName = context.Controller.GetType().Name;

            Log.Information("Request to {Controller}.{Action} with parameters: {@Parameters}",
                controllerName, actionName, context.ActionArguments);

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
