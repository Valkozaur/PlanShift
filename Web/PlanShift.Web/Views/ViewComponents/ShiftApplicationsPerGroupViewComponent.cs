﻿namespace PlanShift.Web.Views.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.Tools.SessionExtension;
    using PlanShift.Web.ViewModels.Shift;

    public class ShiftApplicationsPerGroupViewComponent : ViewComponent
    {
        private readonly IShiftService shiftService;

        public ShiftApplicationsPerGroupViewComponent(IShiftService shiftService)
        {
            this.shiftService = shiftService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string groupId)
        {
            var shiftApplications = await this.shiftService.GetPendingShiftsPerGroupAsync<ShiftWithApplicationsViewModel>(groupId);

            var listOfApplications = new ShiftWithApplicationsListViewModel()
            {
                ShiftsWithApplications = shiftApplications,
            };

            return this.View(listOfApplications);
        }
    }
}
