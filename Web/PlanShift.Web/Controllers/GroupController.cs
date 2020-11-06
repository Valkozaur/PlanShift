﻿namespace PlanShift.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Services.Data.BusinessServices;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Web.ViewModels.Business;
    using PlanShift.Web.ViewModels.Group;

    public class GroupController : Controller
    {
        private readonly IGroupService groupService;
        private readonly IBusinessService businessService;

        public GroupController(IGroupService groupService, IBusinessService businessService)
        {
            this.groupService = groupService;
            this.businessService = businessService;
        }

        public async Task<IActionResult> All(string businessId)
        {
            var groups = await this.groupService.GetAllByBusinessIdAsync<GroupAllViewModel>(businessId);
            var viewModel = new GroupListViewModel()
            {
                Groups = groups,
                BusinessId = businessId,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Create(string businessId)
        {
            var business = await this.businessService.GetBusinessAsync<BusinessInfoViewModel>(businessId);
            var viewModel = new GroupInputModel()
            {
                BusinessName = business.Name,
                BusinessId = business.Id,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var groupId = await this.groupService.CreateGroupAsync(inputModel.BusinessId, inputModel.Name, inputModel.StandardSalary);

            return this.RedirectToAction("All", "Business", new { BusinessId = inputModel.BusinessId });
        }
    }
}
