namespace PlanShift.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Services.Data.BusinessTypeServices;
    using PlanShift.Web.ViewModels.BusinessType;

    public class BusinessTypeController : Controller
    {
        private readonly IBusinessTypeService businessTypeService;

        public BusinessTypeController(IBusinessTypeService businessTypeService)
        {
            this.businessTypeService = businessTypeService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(BusinessTypeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var businessTypeId = await this.businessTypeService.CreateAsync(input.Name);

            return this.RedirectToAction("Register", "Business", new { id = businessTypeId});
        }
    }
}
