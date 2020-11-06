using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanShift.Data.Models;
using PlanShift.Services.Data.EmployeeGroupServices;
using PlanShift.Web.ViewModels.EmployeeGroup;

namespace PlanShift.Web.Controllers
{
    public class EmployeeGroupController : Controller
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

        public async Task<IActionResult> AddEmployeeToGroup(string groupId, EmployeeToGroupInvitationInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.Users.Where(x => x.UserName == input.Username).FirstOrDefaultAsync();

            var employeGroupId = await this.employeeGroupService.AddEmployeeToGroupAsync();

        }

        public async Task<IActionResult> GetGroupManagement(string groupId)
        {
            var management = await this.employeeGroupService.GetAllEmployeesFromGroup<ManagementViewModel>(groupId);
            var viewModel = new ManagementListViewModel()
            {
                Managers = management,
            };

            return this.View(viewModel);
        }
    }
}
