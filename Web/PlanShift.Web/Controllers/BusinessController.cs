namespace PlanShift.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Services.Data.BusinessServices;
    using PlanShift.Services.Data.BusinessTypeServices;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.ShiftApplicationServices;
    using PlanShift.Services.Data.ShiftChangeServices;
    using PlanShift.Web.Tools.ActionFilters;
    using PlanShift.Web.Tools.SessionExtension;
    using PlanShift.Web.ViewModels.Business;
    using PlanShift.Web.ViewModels.BusinessType;

    [Authorize]
    public class BusinessController : BaseController
    {
        private readonly IBusinessService businessService;
        private readonly IBusinessTypeService businessTypeService;
        private readonly IShiftApplicationService shiftApplicationService;
        private readonly IShiftChangeService shiftChangeService;
        private readonly IEmployeeGroupService employeeGroupService;

        public BusinessController(
            IBusinessService businessService,
            IBusinessTypeService businessTypeService,
            IShiftApplicationService shiftApplicationService,
            IShiftChangeService shiftChangeService,
            IEmployeeGroupService employeeGroupService)
        {
            this.businessService = businessService;
            this.businessTypeService = businessTypeService;
            this.shiftApplicationService = shiftApplicationService;
            this.shiftChangeService = shiftChangeService;
            this.employeeGroupService = employeeGroupService;
        }

        [GetSessionInformation(GlobalConstants.BusinessIdSessionName)]
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var businessId = this.HttpContext.Items[GlobalConstants.BusinessIdSessionName].ToString();

            var isScheduleManagerOrAdmin = await this.employeeGroupService.IsEmployeeInGroupsWithNames(userId, businessId, GlobalConstants.AdminsGroupName, GlobalConstants.ScheduleManagersGroupName);

            var viewModel = new BusinessIndexViewModel();

            if (isScheduleManagerOrAdmin)
            {
                var applicationsCount = await this.shiftApplicationService.GetCountOfPendingApplicationsByBusinessIdAsync(businessId);
                var shiftChangesCount = await this.shiftChangeService.GetCountByBusinessIdAsync(businessId);

                viewModel.ShiftApplicationsCount = applicationsCount;
                viewModel.ShiftChangesCount = shiftChangesCount;
            }

            viewModel.IsScheduleManagerOrAdmin = isScheduleManagerOrAdmin;
            viewModel.BusinessId = businessId;

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

        [HttpPost]
        public async Task<IActionResult> Register(BusinessRegisterInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var businessId = await this.businessService.CreateBusinessAsync(userId, inputModel.Name, inputModel.BusinessTypeId);

            if (string.IsNullOrEmpty(await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessIdSessionName)))
            {
                await this.HttpContext.Session.SetStringAsync(GlobalConstants.BusinessIdSessionName, businessId);
                await this.HttpContext.Session.SetStringAsync(GlobalConstants.BusinessNameSessionName, inputModel.Name);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Pick()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var businesses = await this.businessService.GetAllForUserAsync<BusinessAllViewModel>(userId);
            var businessesList = new BusinessListViewModel()
            {
                Businesses = businesses,
            };

            return this.View(businessesList);
        }

        [HttpPost]
        public async Task<IActionResult> Pick(string businessId)
        {
            var businessNameModel = await this.businessService.GetBusinessAsync<BusinessNameViewModel>(businessId);

            await this.HttpContext.Session.SetStringAsync(GlobalConstants.BusinessIdSessionName, businessId);
            await this.HttpContext.Session.SetStringAsync(GlobalConstants.BusinessNameSessionName, businessNameModel.Name);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
