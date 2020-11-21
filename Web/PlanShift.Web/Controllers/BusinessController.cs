﻿namespace PlanShift.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.BusinessServices;
    using PlanShift.Services.Data.BusinessTypeServices;
    using PlanShift.Services.Data.ShiftApplication;
    using PlanShift.Services.Data.ShiftChangeServices;
    using PlanShift.Web.ViewModels.Business;

    public class BusinessController : BaseController
    {
        private readonly IBusinessService businessService;
        private readonly IBusinessTypeService businessTypeService;
        private readonly IShiftApplicationService shiftApplicationService;
        private readonly IShiftChangeService shiftChangeService;
        private readonly UserManager<PlanShiftUser> userManager;

        public BusinessController(
            IBusinessService businessService,
            IBusinessTypeService businessTypeService,
            IShiftApplicationService shiftApplicationService,
            IShiftChangeService shiftChangeService,
            UserManager<PlanShiftUser> userManager)
        {
            this.businessService = businessService;
            this.businessTypeService = businessTypeService;
            this.shiftApplicationService = shiftApplicationService;
            this.shiftChangeService = shiftChangeService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string businessId)
        {
            var applicationsCount = await this.shiftApplicationService.GetCountByBusinessIdAsync(businessId);
            var shiftChangesCount = await this.shiftChangeService.GetCountByBusinessIdAsync(businessId);

            var viewModel = new BusinessIndexViewModel()
            {
                ShiftApplicationsCount = applicationsCount,
                ShiftChangesCount = shiftChangesCount,
                BusinessId = businessId,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Register(int? id)
        {
            var businessTypes = await this.businessTypeService.GetAllAsync<BusinessTypeDropDownViewModel>();
            var viewModel = new BusinessRegisterInputModel()
            {
                BusinessTypes = businessTypes,
                BusinessTypeId = id ?? default,
            };

            return this.View(viewModel);
        }

        // TODO: Finish this action
        [HttpPost]
        public async Task<IActionResult> Register(BusinessRegisterInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value; ;

            var businessId = await this.businessService.CreateBusinessAsync(userId, inputModel.Name, inputModel.BusinessTypeId);

            return this.Json(businessId);
        }

        public async Task<IActionResult> All()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var businesses = await this.businessService.GetAllForUserAsync<BusinessAllViewModel>(userId);
            var businessesList = new BusinessListViewModel()
            {
                Businesses = businesses,
            };

            return this.View(businessesList);
        }
    }
}
