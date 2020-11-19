using PlanShift.Services.Data.ShiftServices;
using PlanShift.Web.ViewModels.Shift;

namespace PlanShift.Web.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Services.Data.ShiftApplication;
    using PlanShift.Web.ViewModels.ShiftApplication;

    public class ShiftApplicationsPerGroupViewComponent : ViewComponent
    {
        private readonly IShiftService shiftService;

        public ShiftApplicationsPerGroupViewComponent(IShiftService shiftService)
        {
            this.shiftService = shiftService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string groupId)
        {
            var shiftApplications = await this.shiftService.GetPendingShiftsPerGroup(groupId);

            var listOfApplications = new ShiftWithApplicationsListViewModel()
            {
                ShiftsWithApplications = shiftApplications,
            };
            
            // TODO: Implement different tab for every group there is in a business;

            return this.View(listOfApplications);
        }
    }
}
