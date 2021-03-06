﻿namespace PlanShift.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using PlanShift.Common;
    using PlanShift.Data.Models.Enumerations;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.Enumerations;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Services.Data.ShiftChangeServices;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.Tools.ActionFilters;
    using PlanShift.Web.Tools.SessionExtension;
    using PlanShift.Web.ViewModels.Group;
    using PlanShift.Web.ViewModels.Shift;
    using PlanShift.Web.ViewModels.ShiftChange;

    [Authorize]
    public class ShiftChangeController : BaseController
    {
        private readonly IShiftChangeService shiftChangeService;
        private readonly IShiftService shiftService;
        private readonly IEmployeeGroupService employeeGroupService;
        private readonly IGroupService groupService;

        public ShiftChangeController(
            IShiftChangeService shiftChangeService,
            IShiftService shiftService,
            IEmployeeGroupService employeeGroupService,
            IGroupService groupService)
        {
            this.shiftChangeService = shiftChangeService;
            this.shiftService = shiftService;
            this.employeeGroupService = employeeGroupService;
            this.groupService = groupService;
        }

        public async Task<IActionResult> Apply(string shiftId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var shiftInformation = await this.shiftService.GetShiftByIdAsync<ShiftInfoViewModel>(shiftId);

            var employeeGroupId = await this.employeeGroupService.GetEmployeeId(userId, shiftInformation.GroupId);

            if (employeeGroupId == null)
            {
                this.ModelState.AddModelError("Error", "You are not participant of the group!");
                return this.RedirectToAction("Index", "Business");
            }

            if (employeeGroupId == shiftInformation.OriginalEmployeeId)
            {
                this.ModelState.AddModelError("Error", "You can't apply for a shift that is already yours!");
            }

            await this.shiftChangeService.CreateShiftChangeAsync(shiftId, shiftInformation.OriginalEmployeeId, employeeGroupId);
            await this.shiftService.StatusChangeAsync(shiftId, ShiftStatus.Pending);

            return this.RedirectToAction("Index", "Business", new { GroupId = shiftInformation.GroupId });
        }

        public async Task<IActionResult> TakeAction(string shiftChangeId, string groupId, bool isAccepted)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                await this.shiftChangeService.AcceptShiftChangeByOriginalEmployeeAsync(userId, shiftChangeId, isAccepted);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("Error", e.Message);
            }

            return this.RedirectToAction("Index", "Business");
        }

        [GetSessionInformation(GlobalConstants.BusinessIdSessionName)]
        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.HrGroupName } })]
        public async Task<IActionResult> Approve(string shiftChangeId, string groupId)
        {
            var businessId = this.HttpContext.Items[GlobalConstants.BusinessIdSessionName].ToString();
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var manager = await this.employeeGroupService.GetEmployeeId(userId, groupId);

            var shiftChange = await this.shiftChangeService.GetShiftChangeByIdAsync<ShiftChangeInfoViewModel>(shiftChangeId);

            await this.shiftService.ApproveShiftToEmployeeAsync(shiftChange.ShiftId, shiftChange.PendingEmployeeId, manager);
            await this.shiftChangeService.ApproveShiftChangeAsync(shiftChangeId, manager);

            return this.RedirectToAction(nameof(this.All), new { BusinessId = businessId, ActiveTabGroupId = groupId });
        }

        public async Task<IActionResult> ShiftSwapRequests(string shiftId)
        {
            var shiftChanges = await this.shiftChangeService.GetShiftChangesPerShiftAsync<ShiftChangeUserViewModel>(shiftId);

            var viewModel = new ShiftChangeListViewModel<ShiftChangeUserViewModel>()
            {
                ShiftChanges = shiftChanges,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> All(string activeTabGroupId)
        {
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessIdSessionName);

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var groupsInBusiness = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupBasicInfoViewModel>(
                businessId,
                userId, 
                true,
                PendingActionsType.ShiftChanges);
            var viewModel = new GroupListViewModel<GroupBasicInfoViewModel>()
            {
                Groups = groupsInBusiness,
                ActiveTabGroupId = activeTabGroupId ?? groupsInBusiness.FirstOrDefault()?.Id,
            };

            return this.View(viewModel);
        }

        public IActionResult SwitchToTabs(string activeTabGroupId)
        {
            return this.RedirectToAction(nameof(this.All), new { ActiveTabGroupId = activeTabGroupId });
        }
    }
}
