namespace PlanShift.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.ShiftChangeServices;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.ViewModels.Shift;

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
    }
}
