namespace PlanShift.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.Enumerations;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Services.Data.ShiftChangeServices;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.Tools.SessionExtension;
    using PlanShift.Web.ViewModels.EmployeeGroup;
    using PlanShift.Web.ViewModels.Group;
    using PlanShift.Web.ViewModels.Shift;
    using PlanShift.Web.ViewModels.ShiftChange;

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

        public IActionResult ApplyForShiftChange(string shiftId)
        {
            return this.View(null, shiftId);
        }

        [HttpPost]
        [ActionName(nameof(ApplyForShiftChange))]
        public async Task<IActionResult> ApplyForShiftChangePost(string shiftId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var shiftInformation = await this.shiftService.GetShiftById<ShiftInfoViewModel>(shiftId);

            var employeeGroupId = await this.employeeGroupService.GetEmployeeId(userId, shiftInformation.GroupId);

            if (employeeGroupId == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (employeeGroupId == shiftInformation.OriginalEmployeeId)
            {
                this.ModelState.AddModelError("User", "You can't apply for a shift that is already yours!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(null, shiftId);
            }

            await this.shiftChangeService.CreateShiftChangeAsync(shiftId, shiftInformation.OriginalEmployeeId, employeeGroupId);

            return this.RedirectToAction("All", "Shift", new { GroupId = shiftInformation.GroupId });
        }

        public async Task<IActionResult> All(string activeTabGroupId)
        {
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessSessionName);

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var groupsInBusiness = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupBasicInfoViewModel>(businessId, userId, false, PendingActionsType.ShiftChanges);
            var viewModel = new GroupListViewModel<GroupBasicInfoViewModel>()
             {
                 Groups = groupsInBusiness,
                 ActiveTabGroupId = activeTabGroupId ?? groupsInBusiness.FirstOrDefault()?.Id,
             };

            return this.View(viewModel);
        }

        public IActionResult SwitchToTabs(string activeTabGroupId)
        {
            return this.RedirectToAction(nameof(this.All), new { ActiveTabGroupId = activeTabGroupId});
        }

        public async Task<IActionResult> Approve(string shiftChangeId, string groupId)
        {
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessSessionName);

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var manager = await this.employeeGroupService.GetEmployeeGroupById<EmployeeGroupInfo>(userId, groupId);
            if (!manager.IsManagement)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var shiftChange = await this.shiftChangeService.GetShiftChangeById<ShiftChangeInfoViewModel>(shiftChangeId);

            await this.shiftService.ApproveShiftToEmployee(shiftChange.ShiftId, shiftChange.PendingEmployeeId, manager.Id);
            await this.shiftChangeService.ApproveShiftChange(shiftChangeId, manager.Id);

            return this.RedirectToAction(nameof(this.All), new { BusinessId = businessId, ActiveTabGroupId = groupId });
        }
    }
}
