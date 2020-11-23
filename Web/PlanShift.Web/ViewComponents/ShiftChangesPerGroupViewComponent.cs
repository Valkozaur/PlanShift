﻿namespace PlanShift.Web.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Services.Data.ShiftChangeServices;
    using PlanShift.Web.ViewModels.ShiftChange;

    public class ShiftChangesPerGroupViewComponent : ViewComponent
    {
        private readonly IShiftChangeService shiftChangeService;

        public ShiftChangesPerGroupViewComponent(IShiftChangeService shiftChangeService)
        {
            this.shiftChangeService = shiftChangeService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string groupId, string businessId)
        {
            var shiftChangesPerGroup = await this.shiftChangeService.GetShiftChangesPerGroupAsync<ShiftChangeAllViewModel>(groupId);
            var viewModel = new ShiftChangeListViewModel<ShiftChangeAllViewModel>()
            {
                BusinessId = businessId,
                GroupId = groupId,
                ShiftChanges = shiftChangesPerGroup,
            };

            return this.View(viewModel);
        }
    }
}