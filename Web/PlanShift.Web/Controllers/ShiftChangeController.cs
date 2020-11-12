using PlanShift.Web.ViewModels.Shift;

namespace PlanShift.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.ShiftChangeServices;
    using PlanShift.Services.Data.ShiftServices;

    public class ShiftChangeController : Controller
    {
        private readonly IShiftChangeService shiftChangeService;
        private readonly IShiftService shiftService;
        private readonly IEmployeeGroupService employeeGroupService;
        private readonly UserManager<PlanShiftUser> userManager;

        public ShiftChangeController(
            IShiftChangeService shiftChangeService,
            IShiftService shiftService,
            IEmployeeGroupService employeeGroupService,
            UserManager<PlanShiftUser> userManager)
        {
            this.shiftChangeService = shiftChangeService;
            this.shiftService = shiftService;
            this.employeeGroupService = employeeGroupService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> ApplyForChange(string shiftId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var shiftInformation = await this.shiftService.GetShiftById<ShiftInfoViewModel>(shiftId);

            var employeeGroupId = await this.employeeGroupService.GetEmployeeId(userId, shiftInformation.GroupId);

            if (employeeGroupId == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            await this.shiftChangeService.CreateShiftChangeAsync(shiftId, shiftInformation.OriginalEmployeeId, userId);

            return this.RedirectToAction("All", "Shift", new { GroupId = shiftInformation.GroupId });
        }
    }
}
