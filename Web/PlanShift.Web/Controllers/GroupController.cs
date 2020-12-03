namespace PlanShift.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.BusinessServices;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Web.Tools.SessionExtension;
    using PlanShift.Web.ViewModels.Business;
    using PlanShift.Web.ViewModels.Group;

    public class GroupController : BaseController
    {
        private readonly IGroupService groupService;
        private readonly IBusinessService businessService;
        private readonly UserManager<PlanShiftUser> userManager;


        public GroupController(IGroupService groupService, IBusinessService businessService, UserManager<PlanShiftUser> userManager)
        {
            this.groupService = groupService;
            this.businessService = businessService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All()
        {
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessSessionName);
            var userId = this.userManager.GetUserId(this.User);

            var groups = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupAllViewModel>(businessId, userId);
            var viewModel = new GroupListViewModel<GroupAllViewModel>()
            {
                Groups = groups,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessSessionName);

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
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessSessionName);


            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var groupId = await this.groupService.CreateGroupAsync(businessId, inputModel.Name, inputModel.StandardSalary);

            return this.RedirectToAction("All", "Business");
        }
    }
}
