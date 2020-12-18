namespace PlanShift.Web.Views.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Services.Data.ShiftChangeServices;
    using PlanShift.Web.Tools.SessionExtension;
    using PlanShift.Web.ViewModels.ShiftChange;

    public class ShiftChangesPerGroupViewComponent : ViewComponent
    {
        private readonly IShiftChangeService shiftChangeService;

        public ShiftChangesPerGroupViewComponent(IShiftChangeService shiftChangeService)
        {
            this.shiftChangeService = shiftChangeService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string groupId)
        {
            var shiftChangesPerGroup = await this.shiftChangeService.GetShiftChangesPerGroupAsync<ShiftChangeManagementViewViewModel>(groupId);
            var viewModel = new ShiftChangeListWithActiveGroupViewModel<ShiftChangeManagementViewViewModel>()
            {
                GroupId = groupId,
                ShiftChanges = shiftChangesPerGroup,
            };

            return this.View(viewModel);
        }
    }
}
