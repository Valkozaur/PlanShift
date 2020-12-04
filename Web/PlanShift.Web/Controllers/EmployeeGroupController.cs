namespace PlanShift.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Web.ViewModels.EmployeeGroup;

    public class EmployeeGroupController : BaseController
    {
        private readonly IEmployeeGroupService employeeGroupService;
        private readonly UserManager<PlanShiftUser> userManager;

        public EmployeeGroupController(
            IEmployeeGroupService employeeGroupService, 
            UserManager<PlanShiftUser> userManager)
        {
            this.employeeGroupService = employeeGroupService;
            this.userManager = userManager;
        }

        public IActionResult AddEmployeeToGroup(string groupId)
        {
            var inputModel = new EmployeeToGroupInvitationInputModel()
            {
                GroupId = groupId,
            };

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeToGroup(EmployeeToGroupInvitationInputModel input)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var isUserManager = await this.employeeGroupService.IsEmployeeManagerInGroup(userId, input.GroupId);

            if (!isUserManager)
            {
                this.ModelState.AddModelError("NotPermitted", "You should be manager to finish this action!");
                return this.View();
            }

            var userToAdd = await this.userManager.FindByEmailAsync(input.Email);

            if (userToAdd == null)
            {
                this.TempData["No Such Employee"] = "User with this email does not exists.";
                return this.RedirectToAction("Index", "InviteUser", input);
            }

            var isEmployeeInTheGroupAlready = await this.employeeGroupService.IsEmployeeInGroup(userToAdd.Id, input.GroupId);

            if (isEmployeeInTheGroupAlready)
            {
                this.ModelState.AddModelError("EmployeeIsInGroupAlready", $"The employee with email:{input.Email} is already in the group!");
                return this.View();
            }

            return this.RedirectToAction("Index", "People", new { ActiveTabGroupId = input.GroupId });
        }
    }
}
