namespace PlanShift.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Services.Data.BusinessTypeServices;
    using PlanShift.Web.ViewModels.BusinessType;

    [Authorize]
    public class BusinessTypeController : BaseController
    {
        private readonly IBusinessTypeService businessTypeService;

        public BusinessTypeController(IBusinessTypeService businessTypeService)
        {
            this.businessTypeService = businessTypeService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BusinessTypeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var businessTypeId = await this.businessTypeService.CreateAsync(input.Name);

            return this.RedirectToAction("Register", "Business", new { id = businessTypeId });
        }
    }
}
