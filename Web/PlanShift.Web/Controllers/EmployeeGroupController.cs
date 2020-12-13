namespace PlanShift.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Web.Tools.ActionFilters;
    using PlanShift.Web.ViewModels.EmployeeGroup;

    [Authorize]
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

        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.HrGroupName } })]
        public IActionResult AddEmployeeToGroup(string groupId)
        {
            var inputModel = new EmployeeToGroupInvitationInputModel()
            {
                GroupId = groupId,
            };

            return this.View(inputModel);
        }

        [HttpPost]
        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.HrGroupName } })]
        public async Task<IActionResult> AddEmployeeToGroup(EmployeeToGroupInvitationInputModel input)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userToAdd = await this.userManager.FindByEmailAsync(input.Email);

            if (userToAdd == null)
            {
                this.ModelState.AddModelError("NoSuchUser", "User with this email does not exists.");
                return this.View(input);
            }

            var isEmployeeInTheGroupAlready = await this.employeeGroupService.IsEmployeeInGroup(userToAdd.Id, input.GroupId);

            if (isEmployeeInTheGroupAlready)
            {
                this.ModelState.AddModelError("EmployeeIsInGroupAlready", $"The employee with email:{input.Email} is already in the group!");
                return this.View();
            }

            await this.employeeGroupService.AddEmployeeToGroupAsync(userToAdd.Id, input.GroupId, input.Salary, input.Position);
            return this.RedirectToAction("Index", "People", new { ActiveTabGroupId = input.GroupId });
        }
    }
}
