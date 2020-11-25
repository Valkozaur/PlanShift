namespace PlanShift.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.ViewModels.Shift;

    [ApiController]
    [Route("api/[controller]")]
    public class GetEventsController : BaseController
    {
        private readonly IShiftService shiftService;

        public GetEventsController(IShiftService shiftService)
        {
            this.shiftService = shiftService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get(string businessId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shifts = await this.shiftService.GetUpcomingShiftForUser<ShiftCalendarViewModel>(businessId, userId);

            return this.Ok();
        }
    }
}
