﻿namespace PlanShift.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.BusinessServices;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Web.Tools.ActionFilters;
    using PlanShift.Web.Tools.SessionExtension;
    using PlanShift.Web.ViewModels.Business;
    using PlanShift.Web.ViewModels.Group;

    [Authorize]
    public class GroupController : BaseController
    {
        private readonly IGroupService groupService;
        private readonly IBusinessService businessService;

        public GroupController(IGroupService groupService, IBusinessService businessService, UserManager<PlanShiftUser> userManager)
        {
            this.groupService = groupService;
            this.businessService = businessService;
        }

        [SessionValidation(GlobalConstants.BusinessIdSessionName)]
        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.HrGroupName } })]
        public async Task<IActionResult> Create()
        {
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessIdSessionName);

            var business = await this.businessService.GetBusinessAsync<BusinessInfoViewModel>(businessId);
            var viewModel = new GroupInputModel()
            {
                BusinessName = business.Name,
                BusinessId = business.Id,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [SessionValidation(GlobalConstants.BusinessIdSessionName)]
        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.HrGroupName } })]
        public async Task<IActionResult> Create(GroupInputModel inputModel)
        {
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessIdSessionName);

            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var groupId = await this.groupService.CreateGroupAsync(businessId, inputModel.Name, inputModel.StandardSalary);

            return this.RedirectToAction("Index", "People", new { GroupId = groupId });
        }

        [Authorize]
        public async Task<IActionResult> GroupChat(string groupId)
        {
            var viewModel = await this.groupService.GetGroupAsync<GroupChatViewModel>(groupId);

            return this.View(viewModel);
        }
    }
}
