namespace PlanShift.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Services.Data.PlacesServices;
    using PlanShift.Web.Tools.ActionFilters;
    using PlanShift.Web.Tools.SessionExtension;
    using PlanShift.Web.ViewModels.Places;

    public class PlaceController : BaseController
    {
        private readonly IPlaceService placeService;

        public PlaceController(IPlaceService placeService)
        {
            this.placeService = placeService;
        }

        [SessionValidation(GlobalConstants.BusinessIdSessionName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [SessionValidation(GlobalConstants.BusinessIdSessionName)]
        public async Task<IActionResult> Create(PlaceInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessIdSessionName);

            var placeId = await this.placeService.CreateAsync(inputModel.Name, businessId);

            return this.RedirectToAction("Create", "Event", new { id = placeId });
        }
    }
}
