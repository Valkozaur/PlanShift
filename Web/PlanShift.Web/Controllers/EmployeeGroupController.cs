namespace PlanShift.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Web.ViewModels.EmployeeGroup;

    public class EmployeeGroupController : BaseController
    {
        private readonly IEmployeeGroupService employeeGroupService;
        private readonly UserManager<PlanShiftUser> userManager;

        public EmployeeGroupController(IEmployeeGroupService employeeGroupService, UserManager<PlanShiftUser> userManager)
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

            var user = await this.userManager.Users.Where(x => x.UserName == input.Username).FirstOrDefaultAsync();

            var isUserAdministrator = await this.userManager.IsInRoleAsync(await this.userManager.GetUserAsync(this.User), "Administrator");

            var employeGroupId = string.Empty;

            if (isUserAdministrator)
            {
                employeGroupId = await this.employeeGroupService.AddEmployeeToGroupAsync(user.Id, input.GroupId, input.Salary, input.Position, input.IsManagement);
            }
            else
            {
                employeGroupId = await this.employeeGroupService.AddEmployeeToGroupAsync(user.Id, input.GroupId, input.Salary, input.Position);
            }

            return this.RedirectToAction(nameof(this.GroupEmployees), new { input.GroupId });
        }

        // TODO: Group Authentication if you can see this info
        public async Task<IActionResult> GroupManagement(string groupId)
        {
            var management = await this.employeeGroupService.GetAllEmployeesFromGroup<ManagementViewModel>(groupId, true);
            var viewModel = new ManagementListViewModel()
            {
                Managers = management,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> GroupEmployees(string groupId)
        {
            var employees = await this.employeeGroupService.GetAllEmployeesFromGroup<EmployeeGroupInfoViewModel>(groupId);
            var viewModel = new EmployeeGroupAllListViewModel()
            {
                Employees = employees,
            };

            return this.View(viewModel);
        }
    }
}
