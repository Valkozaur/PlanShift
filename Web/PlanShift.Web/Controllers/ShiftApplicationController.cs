using PlanShift.Common;
using PlanShift.Web.Tools.SessionExtension;

namespace PlanShift.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Data.Models;
    using PlanShift.Data.Models.Enumerations;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.Enumerations;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Services.Data.ShiftApplication;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.ViewModels.EmployeeGroup;
    using PlanShift.Web.ViewModels.Group;
    using PlanShift.Web.ViewModels.ShiftApplication;

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

        [HttpPost]
        public async Task<IActionResult> Apply(string shiftId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var groupId = await this.shiftService.GetGroupIdAsync(shiftId);

            var employeeId = await this.employeeGroupService.GetEmployeeId(userId, groupId);

            if (employeeId == null)
            {
                return this.RedirectToAction("Index", "Business");
            }

            var hasEmployeeApplied = await this.shiftApplicationService.HasEmployeeAppliedForShift(shiftId, employeeId);
            if (hasEmployeeApplied)
            {
                return this.RedirectToAction("Index", "Business", new { GroupId = groupId });
            }

            // TODO: Make the achievement system check here
            await this.shiftApplicationService.CreateShiftApplicationAsync(shiftId, employeeId);
            await this.shiftService.StatusChange(shiftId, ShiftStatus.Pending);
            return this.RedirectToAction("Index", "Business", new { GroupId = groupId });
        }

        public async Task<IActionResult> Approve(string shiftApplicationId, string businessId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shiftApplicationInfo = await this.shiftApplicationService.GetShiftApplicationById<ApproveShiftInfo>(shiftApplicationId);

            var manager = await this.employeeGroupService.GetEmployeeGroupById<EmployeeGroupInfo>(userId, shiftApplicationInfo.GroupId);
            if (!manager.IsManagement)
            {
                return this.RedirectToAction("Index", "Home");
            }

            await this.shiftService.ApproveShiftToEmployee(shiftApplicationInfo.ShiftId, shiftApplicationInfo.EmployeeId, manager.Id);
            await this.shiftApplicationService.ApproveShiftApplicationAsync(shiftApplicationId);

            return this.RedirectToAction(nameof(this.All), new { BusinessId = businessId, activeTabGroupId = shiftApplicationInfo.GroupId });
        }

        public async Task<IActionResult> All(string activeTabGroupId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessSessionName);

            var groupsInBusiness = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupBasicInfoViewModel>(businessId, userId, false, PendingActionsType.ShiftApplications);

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
