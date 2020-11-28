namespace PlanShift.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.ViewModels.EmployeeGroup;
    using PlanShift.Web.ViewModels.Group;
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

        public async Task<IActionResult> Schedule(string businessId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var groups = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupAllViewModel>(businessId, userId, true);
            var viewModel = new CreateShiftInputModel()
            {
                BusinessId = businessId,
                Groups = groups,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Schedule(CreateShiftInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var employeeGroup = await this.employeeGroupService.GetEmployeeGroupById<EmployeeGroupIsManagementInfo>(userId, input.GroupId);

            var groups = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupAllViewModel>(input.BusinessId, userId, true);

            input.Groups = groups;
            

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (employeeGroup.IsManagement)
            {
                await this.shiftService.CreateShift(employeeGroup.Id, input.GroupId, input.Start, input.End, input.Description, input.BonusPayment ?? 0);
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }

            var groupsBusinessId = await this.groupService.GetGroupsBusinessId(input.GroupId);

            return this.View(input);
        }

        public IActionResult CreateFormViewComponent(string groupId)
        {
            return this.ViewComponent("CreateShift", new { groupId });
        }
    }
}