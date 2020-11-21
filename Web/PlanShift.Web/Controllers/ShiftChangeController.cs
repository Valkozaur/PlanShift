namespace PlanShift.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.Enumerations;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Services.Data.ShiftChangeServices;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.ViewModels.Group;
    using PlanShift.Web.ViewModels.Shift;

    public class ShiftChangeController : Controller
    {
        private readonly IShiftChangeService shiftChangeService;
        private readonly IShiftService shiftService;
        private readonly IEmployeeGroupService employeeGroupService;
        private readonly IGroupService groupService;
        private readonly UserManager<PlanShiftUser> userManager;

        public ShiftChangeController(
            IShiftChangeService shiftChangeService,
            IShiftService shiftService,
            IEmployeeGroupService employeeGroupService,
            IGroupService groupService,
            UserManager<PlanShiftUser> userManager)
        {
            this.shiftChangeService = shiftChangeService;
            this.shiftService = shiftService;
            this.employeeGroupService = employeeGroupService;
            this.groupService = groupService;
            this.userManager = userManager;
        }

        public IActionResult ApplyForShiftChange(string shiftId)
        {
            return this.View(null, shiftId);
        }

        [HttpPost]
        [ActionName(nameof(ApplyForShiftChange))]
        public async Task<IActionResult> ApplyForShiftChangePost(string shiftId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var shiftInformation = await this.shiftService.GetShiftById<ShiftInfoViewModel>(shiftId);

            var employeeGroupId = await this.employeeGroupService.GetEmployeeId(userId, shiftInformation.GroupId);

            if (employeeGroupId == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (employeeGroupId == shiftInformation.OriginalEmployeeId)
            {
                this.ModelState.AddModelError("Employee", "You can't apply for a shift that is already yours!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(null, shiftId);
            }

            await this.shiftChangeService.CreateShiftChangeAsync(shiftId, shiftInformation.OriginalEmployeeId, employeeGroupId);

            return this.RedirectToAction("All", "Shift", new { GroupId = shiftInformation.GroupId });
        }

        public async Task<IActionResult> All(string businessId, string activeTabGroupId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var groupsInBusiness = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupBasicInfoViewModel>(businessId, userId, PendingActionsType.ShiftChanges);
            var viewModel = new GroupListViewModel<GroupBasicInfoViewModel>()
             {
                 Groups = groupsInBusiness,
                 ActiveTabGroupId = activeTabGroupId ?? groupsInBusiness.FirstOrDefault()?.Id,
                 BusinessId = businessId,
             };

            return this.View(viewModel);
        }

        public IActionResult SwitchToTabs(string activeTabGroupId, string businessId)
        {
            return this.RedirectToAction(nameof(this.All), new { ActiveTabGroupId = activeTabGroupId, businessId = businessId });
        }
    }
}
