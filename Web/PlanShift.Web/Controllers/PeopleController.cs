namespace PlanShift.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Web.Infrastructure.Validations.UserValidationAttributes;
    using PlanShift.Web.Tools.SessionExtension;
    using PlanShift.Web.ViewModels.Group;

    public class PeopleController : BaseController
    {
        private readonly IGroupService groupService;

        public PeopleController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [SessionValidation(GlobalConstants.BusinessSessionName)]
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessSessionName);

            var groups = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupPeopleCountViewModel>(businessId, userId, true);

            var viewModel = new GroupListViewModel<GroupPeopleCountViewModel>()
            {
                Groups = groups,
            };

            return this.View(viewModel);
        }
    }
}
