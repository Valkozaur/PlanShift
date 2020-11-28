using System.Security.Claims;
using PlanShift.Web.ViewModels.Group;

namespace PlanShift.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using PlanShift.Data.Models;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.ViewModels.EmployeeGroup;
    using PlanShift.Web.ViewModels.Shift;

    public class ShiftController : Controller
    {
        private readonly IShiftService shiftService;
        private readonly IEmployeeGroupService employeeGroupService;
        private readonly IGroupService groupService;
        private readonly UserManager<PlanShiftUser> userManager;

        public ShiftController(
            IShiftService shiftService,
            IEmployeeGroupService employeeGroupService,
            IGroupService groupService,
            UserManager<PlanShiftUser> userManager)
        {
            this.shiftService = shiftService;
            this.employeeGroupService = employeeGroupService;
            this.groupService = groupService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var groups = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupAllViewModel>(businessId, userId);
            var viewModel = new GroupListViewModel<GroupAllViewModel>()
            {
                Groups = groups,
                BusinessId = businessId,
                ActiveTabGroupId = null,
            };

            if (inputModel != null)
            {
                viewModel.ShiftInputModel = inputModel;
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateShiftInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var employeeGroup = await this.employeeGroupService.GetEmployeeGroupById<EmployeeGroupIsManagementInfo>(userId, input.GroupId);

            if (employeeGroup.IsManagement)
            {
                await this.shiftService.CreateShift(employeeGroup.Id, input.GroupId, input.Start, input.End, input.Description, input.BonusPayment ?? 0);
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }

            var groupsBusinessId = await this.groupService.GetGroupsBusinessId(input.GroupId);
            input.BusinessId = groupsBusinessId;

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Schedule", "Schedule", input);
            }

            return this.RedirectToAction("Schedule", "Schedule", new { input.BusinessId, input.GroupId });
        }

        public IActionResult CreateFormViewComponent(string groupId)
        {
            return this.ViewComponent("CreateShift", new { groupId });
        }
    }
}