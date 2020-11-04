namespace PlanShift.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.BusinessServices;
    using PlanShift.Web.ViewModels.Business;

    public class BusinessController : BaseController
    {
        private readonly IBusinessService businessService;
        private readonly UserManager<PlanShiftUser> userManager;

        public BusinessController(IBusinessService businessService, UserManager<PlanShiftUser> userManager)
        {
            this.businessService = businessService;
            this.userManager = userManager;
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterBusinessInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = await this.userManager.GetUserAsync(this.User);

            var businessId = await this.businessService.CreateBusinessAsync(userId.Id, inputModel.Name, inputModel.BusinessType);

            return this.Json(businessId);
        }
    }
}
