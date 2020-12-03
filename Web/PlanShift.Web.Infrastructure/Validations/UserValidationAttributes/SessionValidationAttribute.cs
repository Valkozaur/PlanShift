namespace PlanShift.Web.Infrastructure.Validations.UserValidationAttributes
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;

    public class SessionValidationAttribute : ActionFilterAttribute
    {
        private readonly string key;

        public SessionValidationAttribute(string key)
        {
            this.key = key;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (!filterContext.HttpContext.Session.TryGetValue(this.key, out _))
            {
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary
                    {
                        { "action", "Pick" },
                        { "controller", "Business" },
                    });
                return;
            }
        }
    }
}