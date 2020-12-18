namespace PlanShift.Web.Tools.ActionFilters
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;
    using PlanShift.Web.Tools.SessionExtension;

    public class SessionValidationAttribute : ActionFilterAttribute
    {
        private readonly string key;

        public SessionValidationAttribute(string key)
        {
            this.key = key;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var hasUserSessionWithKey = await context.HttpContext.Session.HasKeyAsync(this.key);

            if (!hasUserSessionWithKey)
            {
                context.Result =
                    new RedirectToRouteResult(new RouteValueDictionary
                    {
                        { "action", "Pick" },
                        { "controller", "Business" },
                    });
            }
            else
            {
                await next();
            }
        }
    }
}
