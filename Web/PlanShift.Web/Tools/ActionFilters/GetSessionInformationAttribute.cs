namespace PlanShift.Web.Tools.ActionFilters
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;
    using PlanShift.Web.Tools.SessionExtension;

    public class GetSessionInformationAttribute : ActionFilterAttribute
    {
        private readonly string key;

        public GetSessionInformationAttribute(string key)
        {
            this.key = key;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var sessionValue = await context.HttpContext.Session.GetStringAsync(this.key);

            if (sessionValue == null)
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
                context.HttpContext.Items[this.key] = sessionValue;
                await next();
            }
        }
    }
}
