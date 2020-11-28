namespace PlanShift.Web.Views.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Web.ViewModels.Shift;

    public class CreateShiftViewComponent : ViewComponent
    {
        public CreateShiftViewComponent()
        {
        }

        public IViewComponentResult Invoke(string groupId, [FromHeader]string businessId)
        {
            var model = new CreateShiftInputModel()
            {
                GroupId = groupId,
                BusinessId = null,
            };

            return this.View(model);
        }
    }
}