namespace PlanShift.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.Infrastructure.Validations.UserValidationAttributes;
    using PlanShift.Web.ViewModels.EmployeeGroup;
    using PlanShift.Web.ViewModels.Group;
    using PlanShift.Web.ViewModels.Shift;

    public class ShiftController : BaseController
    {
        private readonly IShiftService shiftService;
        private readonly IEmployeeGroupService employeeGroupService;
        private readonly IGroupService groupService;
        private readonly UserManager<PlanShiftUser> userManager;

        public ShiftController(
            IShiftService shiftService,
            IEmployeeGroupService employeeGroupService,
            IGroupService groupService,
            UserManager<PlanShiftUser> userManager)
        {
            this.shiftService = shiftService;
            this.employeeGroupService = employeeGroupService;
            this.groupService = groupService;
            this.userManager = userManager;
        }

        [SessionValidation(GlobalConstants.BusinessSessionName)]
        public async Task<IActionResult> Schedule()
        {
            var businessId = this.HttpContext.Session.GetString(GlobalConstants.BusinessSessionName);

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var groups = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupAllViewModel>(businessId, userId, true);

            var groupId = string.Empty;
            if (this.TempData["GroupId"] != null)
            {
                groupId = this.TempData["GroupId"].ToString();
            }

            var viewModel = new CreateShiftInputModel()
            {
                BusinessId = businessId,
                Groups = groups,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Schedule(CreateShiftInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var employeeGroup = await this.employeeGroupService.GetEmployeeGroupById<EmployeeGroupIsManagementInfo>(userId, input.GroupId);

            if (!this.ModelState.IsValid)
            {
                var groups = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupAllViewModel>(input.BusinessId, userId, true);
                input.Groups = groups;
                return this.View(input);
            }

            if (employeeGroup.IsManagement)
            {
                await this.shiftService.CreateShift(employeeGroup.Id, input.GroupId, input.Start, input.End, input.Description, input.BonusPayment ?? 0);
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }

            this.TempData["GroupId"] = input.GroupId;
            return this.RedirectToAction("Schedule", new { input.BusinessId });
        }

        public IActionResult CreateFormViewComponent(string groupId)
        {
            return this.ViewComponent("CreateShift", new { groupId });
        }
    }
}