﻿namespace PlanShift.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Web.Tools.ActionFilters;
    using PlanShift.Web.Tools.SessionExtension;
    using PlanShift.Web.ViewModels.Group;

    [Authorize]
    public class PeopleController : BaseController
    {
        private readonly IGroupService groupService;
        private readonly IEmployeeGroupService employeeGroupService;

        public PeopleController(IGroupService groupService, IEmployeeGroupService employeeGroupService)
        {
            this.groupService = groupService;
            this.employeeGroupService = employeeGroupService;
        }

        [GetSessionInformation(GlobalConstants.BusinessIdSessionName)]
        public async Task<IActionResult> Index(string activeTabGroupId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var businessId = this.HttpContext.Items[GlobalConstants.BusinessIdSessionName].ToString();

            var groups = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupPeopleCountViewModel>(businessId, userId);

            var specialGroups = groups
                .Where(x
                    => x.Name == GlobalConstants.AdminsGroupName
                       || x.Name == GlobalConstants.HrGroupName
                       || x.Name == GlobalConstants.ScheduleManagersGroupName);

            groups = groups.Where(x
                => x.Name != GlobalConstants.AdminsGroupName
                   && x.Name != GlobalConstants.HrGroupName
                   && x.Name != GlobalConstants.ScheduleManagersGroupName);

            var viewModel = new GroupListViewModel<GroupPeopleCountViewModel>()
            {
                Groups = groups,
                SpecialGroups = specialGroups,
                ActiveTabGroupId = activeTabGroupId ?? groups.FirstOrDefault()?.Id,
                IsInHrOrAdminRoleGroup = await this.employeeGroupService.IsEmployeeInGroupsWithNames(userId, businessId, GlobalConstants.AdminsGroupName, GlobalConstants.HrGroupName),
            };

            return this.View(viewModel);
        }

        public IActionResult SwitchToTabs(string activeTabGroupId)
        {
            return this.RedirectToAction(nameof(this.Index), new { ActiveTabGroupId = activeTabGroupId });
        }
    }
}
