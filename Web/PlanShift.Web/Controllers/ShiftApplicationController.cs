namespace PlanShift.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.ShiftApplication;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.ViewModels.EmployeeGroup;
    using PlanShift.Web.ViewModels.Shift;
    using PlanShift.Web.ViewModels.ShiftApplication;

    public class ShiftApplicationController : Controller
    {
        private readonly IShiftApplicationService shiftApplicationService;
        private readonly IShiftService shiftService;
        private readonly IEmployeeGroupService employeeGroupService;
        private readonly UserManager<PlanShiftUser> userManager;

        public ShiftApplicationController(
            IShiftApplicationService shiftApplicationService,
            IShiftService shiftService,
            IEmployeeGroupService employeeGroupService,
            UserManager<PlanShiftUser> userManager)
        {
            this.shiftApplicationService = shiftApplicationService;
            this.shiftService = shiftService;
            this.employeeGroupService = employeeGroupService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Apply(string shiftId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var groupId = await this.shiftService.GetGroupIdAsync(shiftId);

            var employeeGroupId = await this.employeeGroupService.GetEmployeeId(userId, groupId);

            if (employeeGroupId == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var hasEmployeeApplied = await this.shiftApplicationService.HasEmployeeAppliedForShift(shiftId, employeeGroupId);
            if (hasEmployeeApplied)
            {
                return this.RedirectToAction("All", "Shift", new { GroupId = groupId });
            }

            // TODO: Make the achievement system check here
            await this.shiftApplicationService.CreateShiftApplicationAsync(shiftId, employeeGroupId);
            return this.RedirectToAction("All", "Shift", new { GroupId = groupId });
        }

        public async Task<IActionResult> All(string shiftId)
        {
            var shiftApplications = await this.shiftApplicationService.GetAllApplicationByShiftIdAsync<ShiftApplicationAllViewModel>(shiftId);
            var shift = await this.shiftService.GetShiftById<ShiftShiftApplicationViewModel>(shiftId);
            var viewModel = new ShiftApplicationListViewModel()
            {
                Applications = shiftApplications,
                Shift = shift,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Approve(string shiftApplicationId)
        {
            var userId = this.userManager.GetUserId(this.User);

            var shiftApplicationInfo = await this.shiftApplicationService.GetShiftApplicationById<ApproveShiftInfo>(shiftApplicationId);

            var manager = await this.employeeGroupService.GetEmployeeGroupById<EmployeeGroupInfo>(userId, shiftApplicationInfo.GroupId);
            if (!manager.IsManagement)
            {
                return this.RedirectToAction("Index", "Home");
            }

            await this.shiftService.ApproveShiftToEmployee(shiftApplicationInfo.ShiftId, shiftApplicationInfo.EmployeeId, manager.Id);
            await this.shiftApplicationService.ApproveShiftApplicationAsync(shiftApplicationId);

            return this.RedirectToAction("All", "Shift", new { GroupId = shiftApplicationInfo.GroupId });
        }
    }
}
