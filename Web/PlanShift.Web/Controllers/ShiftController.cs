namespace PlanShift.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using PlanShift.Common;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.Tools.ActionFilters;
    using PlanShift.Web.ViewModels.Group;
    using PlanShift.Web.ViewModels.Shift;

    [Authorize]
    public class ShiftController : BaseController
    {
        private readonly IShiftService shiftService;
        private readonly IEmployeeGroupService employeeGroupService;
        private readonly IGroupService groupService;

        public ShiftController(
            IShiftService shiftService,
            IEmployeeGroupService employeeGroupService,
            IGroupService groupService)
        {
            this.shiftService = shiftService;
            this.employeeGroupService = employeeGroupService;
            this.groupService = groupService;
        }

        [SessionValidation(GlobalConstants.BusinessIdSessionName)]
        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.ScheduleManagersGroupName } })]
        public async Task<IActionResult> Schedule()
        {
            var businessId = this.HttpContext.Session.GetString(GlobalConstants.BusinessIdSessionName);

            var groups = await this.groupService.GetAllGroupsByBusiness<GroupAllViewModel>(businessId, false);

            if (groups.Count() == 0)
            {
                return this.View("NoAvailableGroups");
            }

            var viewModel = new CreateShiftInputModel()
            {
                BusinessId = businessId,
                Groups = groups,
            };

            this.TempData["GroupId"] = groups.First().Id;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [SessionValidation(GlobalConstants.BusinessIdSessionName)]
        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.ScheduleManagersGroupName } })]

        public async Task<IActionResult> Schedule(CreateShiftInputModel input)
        {

            var businessId = this.HttpContext.Session.GetString(GlobalConstants.BusinessIdSessionName);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var managementId = await this.employeeGroupService.GetFirstEmployeeIdFromAdministrationGroups(userId, businessId);

            if (!this.ModelState.IsValid)
            {
                var groups = await this.groupService.GetAllGroupsByBusiness<GroupAllViewModel>(input.BusinessId, false);
                input.Groups = groups;
                return this.View(input);
            }

            await this.shiftService.CreateShiftAsync(managementId, input.GroupId, input.Start, input.End, input.Description, input.BonusPayment ?? 0);

            this.TempData["GroupId"] = input.GroupId;
            return this.RedirectToAction("Schedule", new { input.BusinessId });
        }

        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.ScheduleManagersGroupName } })]
        public async Task<IActionResult> Delete(string shiftId)
        {
            await this.shiftService.DeleteShift(shiftId);

            return this.RedirectToAction(nameof(this.Schedule));
        }

        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.ScheduleManagersGroupName } })]
        public IActionResult CreateFormViewComponent(string groupId)
        {
            return this.ViewComponent("CreateShiftAsync", new { groupId });
        }
    }
}
