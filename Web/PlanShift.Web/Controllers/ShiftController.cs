namespace PlanShift.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.Tools.ActionFilters;
    using PlanShift.Web.ViewModels.EmployeeGroup;
    using PlanShift.Web.ViewModels.Group;
    using PlanShift.Web.ViewModels.Shift;

    [Authorize]
    public class ShiftController : BaseController
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

        [SessionValidation(GlobalConstants.BusinessSessionName)]
        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.ScheduleManagersGroupName } })]
        public async Task<IActionResult> Schedule()
        {
            var businessId = this.HttpContext.Session.GetString(GlobalConstants.BusinessSessionName);

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var groups = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupAllViewModel>(businessId, userId);

            var viewModel = new CreateShiftInputModel()
            {
                BusinessId = businessId,
                Groups = groups,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.ScheduleManagersGroupName } })]

        public async Task<IActionResult> Schedule(CreateShiftInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var managementId = await this.employeeGroupService.GetEmployeeId(userId, input.GroupId);

            if (!this.ModelState.IsValid)
            {
                var groups = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupAllViewModel>(input.BusinessId, userId);
                input.Groups = groups;
                return this.View(input);
            }

            await this.shiftService.CreateShift(managementId, input.GroupId, input.Start, input.End, input.Description, input.BonusPayment ?? 0);

            this.TempData["GroupId"] = input.GroupId;
            return this.RedirectToAction("Schedule", new { input.BusinessId });
        }

        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.ScheduleManagersGroupName } })]
        public IActionResult CreateFormViewComponent(string groupId)
        {
            return this.ViewComponent("CreateShift", new { groupId });
        }
    }
}