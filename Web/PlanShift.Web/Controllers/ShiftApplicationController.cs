namespace PlanShift.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Data.Models;
    using PlanShift.Data.Models.Enumerations;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.Enumerations;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Services.Data.ShiftApplicationServices;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.Tools.ActionFilters;
    using PlanShift.Web.Tools.SessionExtension;
    using PlanShift.Web.ViewModels.EmployeeGroup;
    using PlanShift.Web.ViewModels.Group;
    using PlanShift.Web.ViewModels.Shift;
    using PlanShift.Web.ViewModels.ShiftApplication;

    [Authorize]
    public class ShiftApplicationController : BaseController
    {
        private readonly IShiftApplicationService shiftApplicationService;
        private readonly IShiftService shiftService;
        private readonly IEmployeeGroupService employeeGroupService;
        private readonly IGroupService groupService;
        private readonly UserManager<PlanShiftUser> userManager;

        public ShiftApplicationController(
            IShiftApplicationService shiftApplicationService,
            IShiftService shiftService,
            IEmployeeGroupService employeeGroupService,
            IGroupService groupService,
            UserManager<PlanShiftUser> userManager)
        {
            this.shiftApplicationService = shiftApplicationService;
            this.shiftService = shiftService;
            this.employeeGroupService = employeeGroupService;
            this.groupService = groupService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Apply(string shiftId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var shiftInformation = await this.shiftService.GetShiftByIdAsync<ShiftIdStatusViewModel>(shiftId);

            var employeeId = await this.employeeGroupService.GetEmployeeId(userId, shiftInformation.GroupId);

            if (employeeId == null)
            {
                this.TempData["Error"] = "You are not participant of the group!";
                return this.RedirectToAction("Index", "Business");
            }

            if (shiftInformation.Status == ShiftStatus.Approved)
            {
                this.TempData["Error"] = "Shift is already taken!";
                return this.RedirectToAction("Index", "Business", new { GroupId = shiftInformation });
            }

            var hasEmployeeApplied = await this.shiftApplicationService.HasEmployeeActiveApplicationForShiftAsync(shiftId, employeeId);
            if (hasEmployeeApplied)
            {
                this.TempData["Error"] = "You've applied for this shift already!";
                return this.RedirectToAction("Index", "Business", new { GroupId = shiftInformation });
            }

            // TODO: Make the achievement system check here
            await this.shiftApplicationService.CreateShiftApplicationAsync(shiftId, employeeId);
            await this.shiftService.StatusChangeAsync(shiftId, ShiftStatus.Pending);
            this.TempData["Success"] = "You've applied for the shift";
            return this.RedirectToAction("Index", "Business", new { GroupId = shiftInformation });
        }

        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.ScheduleManagersGroupName } })]
        public async Task<IActionResult> Approve(string shiftApplicationId, string businessId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var shiftApplicationInfo = await this.shiftApplicationService.GetShiftApplicationById<ApproveShiftInfo>(shiftApplicationId);

            var employeeId = await this.employeeGroupService.GetEmployeeId(userId, shiftApplicationInfo.GroupId);

            await this.shiftService.ApproveShiftToEmployeeAsync(shiftApplicationInfo.ShiftId, shiftApplicationInfo.EmployeeId, employeeId);
            await this.shiftApplicationService.DeclineAllShiftApplicationsPerShiftAsync(shiftApplicationInfo.ShiftId);
            await this.shiftApplicationService.ApproveShiftApplicationAsync(shiftApplicationId);

            return this.RedirectToAction(nameof(this.All), new { BusinessId = businessId, activeTabGroupId = shiftApplicationInfo.GroupId });
        }

        public async Task<IActionResult> All(string activeTabGroupId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessNameSessionName);

            var groupsInBusiness = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupBasicInfoViewModel>(businessId, userId, PendingActionsType.ShiftApplications);

            var viewModel = new GroupListViewModel<GroupBasicInfoViewModel>()
            {
                Groups = groupsInBusiness,
                ActiveTabGroupId = activeTabGroupId ?? groupsInBusiness.FirstOrDefault()?.Id,
            };

            return this.View(viewModel);
        }

        public IActionResult SwitchToTabs(string activeTabGroupId, string businessId)
        {
            return this.RedirectToAction(nameof(this.All), new { ActiveTabGroupId = activeTabGroupId, businessId = businessId });
        }
    }
}
