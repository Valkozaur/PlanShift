namespace PlanShift.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Web.ViewModels.Group;
    using PlanShift.Web.ViewModels.Shift;

    public class ScheduleController : Controller
    {
        private readonly IGroupService groupService;

        public ScheduleController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        public async Task<IActionResult> Schedule(string businessId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var groups = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupAllViewModel>(businessId, userId);
            var viewModel = new GroupListViewModel<GroupAllViewModel>()
            {
                Groups = groups,
                BusinessId = businessId,
                ActiveTabGroupId = null,
            };

            return this.View(viewModel);
        }
    }
}
