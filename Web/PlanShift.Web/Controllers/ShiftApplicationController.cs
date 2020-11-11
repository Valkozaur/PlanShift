namespace PlanShift.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.ShiftApplication;
    using PlanShift.Services.Data.ShiftServices;

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

            //TODO: Make the achievement system check here

            await this.shiftApplicationService.CreateShiftApplicationAsync(shiftId, employeeGroupId);
            return this.RedirectToAction("All", "Shift", new { GroupId = groupId });
        }
    }
}
