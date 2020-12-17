namespace PlanShift.Web.Tools.ActionFilters
{
    using System.Formats.Asn1;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Primitives;
    using PlanShift.Common;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Web.Tools.SessionExtension;

    public class IsEmployeeInRoleGroupAttribute : ActionFilterAttribute
    {
        private readonly IEmployeeGroupService employeeGroupService;
        private readonly string[] roleGroupNames;

        public IsEmployeeInRoleGroupAttribute(string[] roleGroupNames, IEmployeeGroupService employeeGroupService)
        {
            this.employeeGroupService = employeeGroupService;
            this.roleGroupNames = roleGroupNames;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var businessId = await context.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessIdSessionName);

            var isEmployeeInRoleGroup = await this.employeeGroupService.IsEmployeeInGroupsWithNames(userId, businessId, this.roleGroupNames);

            if (!isEmployeeInRoleGroup)
            {
                context.ModelState.AddModelError("Error", "You don't have permission for that!");
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "action", "Index" },
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