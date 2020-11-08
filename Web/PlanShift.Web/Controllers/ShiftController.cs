using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlanShift.Data.Models;
using PlanShift.Services.Data.EmployeeGroupServices;
using PlanShift.Services.Data.GroupServices;
using PlanShift.Services.Data.ShiftServices;
using PlanShift.Web.ViewModels.ControllerDTO;
using PlanShift.Web.ViewModels.Shift;

namespace PlanShift.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ShiftController : Controller
    {
        private readonly IShiftService shiftService;
        private readonly IEmployeeGroupService employeeGroupService;
        private readonly IGroupService groupService;
        private readonly UserManager<PlanShiftUser> userManager;

        public ShiftController(IShiftService shiftService, IEmployeeGroupService employeeGroupService, IGroupService groupService, UserManager<PlanShiftUser> userManager)
        {
            this.shiftService = shiftService;
            this.employeeGroupService = employeeGroupService;
            this.groupService = groupService;
            this.userManager = userManager;
        }

        public IActionResult Create(string groupId)
        {
            var model = new CreateShiftInputModel()
            {
                GroupId = groupId,
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateShiftInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.userManager.GetUserId(this.User);

            var employeeGroup = await this.employeeGroupService.GetEmployeeGroupById<EmployeeGroupIsManagement>(input.GroupId, userId);

            if (employeeGroup.IsManagement)
            {
                await this.shiftService.CreateShift(userId, input.GroupId, input.Start, input.End, input.Description, input.BonusPayment ?? 0);
            }

            return this.RedirectToAction(nameof(this.All), new {GroupId = input.GroupId});
        }

        public async Task<IActionResult> All(string groupId)
        {
            var shifts = await this.shiftService.GetAllShiftsByGroup<ShiftAllViewModel>(groupId);
            var groupName = await this.groupService.GetGroupName(groupId);

            var viewModel = new ShiftListViewModel()
            {
                Shifts = shifts,
                GroupName = groupName,
            };

            return this.View(viewModel);
        }
    }
}
