namespace PlanShift.Web.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.ViewModels.Shift;

    public class ShiftViewComponent : ViewComponent
    {
        private readonly IShiftService shiftService;

        public ShiftViewComponent(IShiftService shiftService)
        {
            this.shiftService = shiftService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string shiftId)
        {
            var viewModel = await this.shiftService.GetShiftById<ShiftShiftApplicationViewModel>(shiftId);

            return this.View(viewModel);
        }

    }
}
