namespace PlanShift.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Data.Models.Enumerations;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.Enumerations;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Services.Data.ShiftChangeServices;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.Tools.ActionFilters;
    using PlanShift.Web.Tools.SessionExtension;
    using PlanShift.Web.ViewModels.EmployeeGroup;
    using PlanShift.Web.ViewModels.Group;
    using PlanShift.Web.ViewModels.Shift;
    using PlanShift.Web.ViewModels.ShiftChange;

    [Authorize]
    public class ShiftChangeController : BaseController
    {
        private readonly IShiftChangeService shiftChangeService;
        private readonly IShiftService shiftService;
        private readonly IEmployeeGroupService employeeGroupService;
        private readonly IGroupService groupService;

        public ShiftChangeController(
            IShiftChangeService shiftChangeService,
            IShiftService shiftService,
            IEmployeeGroupService employeeGroupService,
            IGroupService groupService)
        {
            this.shiftChangeService = shiftChangeService;
            this.shiftService = shiftService;
            this.employeeGroupService = employeeGroupService;
            this.groupService = groupService;
        }

        public async Task<IActionResult> Apply(string shiftId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var shiftInformation = await this.shiftService.GetShiftByIdAsync<ShiftInfoViewModel>(shiftId);

            var employeeGroupId = await this.employeeGroupService.GetEmployeeId(userId, shiftInformation.GroupId);

            if (employeeGroupId == null)
            {
                this.TempData["Error"] = "You are not participant of the group!";
                return this.RedirectToAction("Index", "Business");
            }

            if (employeeGroupId == shiftInformation.OriginalEmployeeId)
            {
                this.TempData["Error"] = "You can't apply for a shift that is already yours!";
            }

            await this.shiftChangeService.CreateShiftChangeAsync(shiftId, shiftInformation.OriginalEmployeeId, employeeGroupId);
            await this.shiftService.StatusChangeAsync(shiftId, ShiftStatus.Pending);

            return this.RedirectToAction("Index", "Business", new { GroupId = shiftInformation.GroupId });
        }

        public async Task<IActionResult> TakeAction(string shiftChangeId, string groupId, bool isAccepted)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                await this.shiftChangeService.ProcessShiftChangeOriginalEmployeeStatus(userId, shiftChangeId, isAccepted);
                this.TempData["Success"] = "Shift swap action taken successfully!";
            }
            catch (Exception e)
            {
                this.TempData["Error"] = e.Message;
            }

            return this.RedirectToAction("Index", "Business");
        }

        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.HrGroupName } })]
        public async Task<IActionResult> Approve(string shiftChangeId, string groupId)
        {
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessSessionName);

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var manager = await this.employeeGroupService.GetEmployeeId(userId, groupId);

            var shiftChange = await this.shiftChangeService.GetShiftChangeById<ShiftChangeInfoViewModel>(shiftChangeId);

            await this.shiftService.ApproveShiftToEmployeeAsync(shiftChange.ShiftId, shiftChange.PendingEmployeeId, manager);
            await this.shiftChangeService.ApproveShiftChange(shiftChangeId, manager);

            return this.RedirectToAction(nameof(this.All), new { BusinessId = businessId, ActiveTabGroupId = groupId });
        }

        public async Task<IActionResult> ShiftSwapRequests(string shiftId)
        {
            var shiftChanges = await this.shiftChangeService.GetShiftChangesPerShift<ShiftChangeUserViewModel>(shiftId);

            var viewModel = new ShiftChangeListViewModel<ShiftChangeUserViewModel>()
            {
                ShiftChanges = shiftChanges,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> All(string activeTabGroupId)
        {
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessSessionName);

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var groupsInBusiness = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupBasicInfoViewModel>(businessId, userId, PendingActionsType.ShiftChanges);
            var viewModel = new GroupListViewModel<GroupBasicInfoViewModel>()
            {
                Groups = groupsInBusiness,
                ActiveTabGroupId = activeTabGroupId ?? groupsInBusiness.FirstOrDefault()?.Id,
            };

            return this.View(viewModel);
        }

        public IActionResult SwitchToTabs(string activeTabGroupId)
        {
            return this.RedirectToAction(nameof(this.All), new { ActiveTabGroupId = activeTabGroupId });
        }
    }
}
