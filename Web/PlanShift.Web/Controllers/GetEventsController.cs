using EllipticCurve;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PlanShift.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Services.Data.ShiftApplication;
    using PlanShift.Services.Data.ShiftChangeServices;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.ViewModels.Enumerations;
    using PlanShift.Web.ViewModels.Shift;

    [ApiController]
    [Route("api/[controller]")]
    public class GetEventsController : BaseController
    {
        private readonly IShiftService shiftService;
        private readonly IShiftChangeService shiftChangeService;
        private readonly IShiftApplicationService shiftApplicationService;

        public GetEventsController(
            IShiftService shiftService,
            IShiftChangeService shiftChangeService,
            IShiftApplicationService shiftApplicationService)
        {
            this.shiftService = shiftService;
            this.shiftChangeService = shiftChangeService;
            this.shiftApplicationService = shiftApplicationService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get(string businessId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var upcomingShifts = await this.shiftService.GetUpcomingShiftForUser<ShiftCalendarViewModel>(businessId, userId);
            var openShifts = await this.shiftService.GetOpenShiftsAvailableForUser<ShiftCalendarViewModel>(businessId, userId);
            var swapRequests = await this.shiftService.GetPendingShiftsPerUser<ShiftCalendarViewModel>(businessId, userId);

            foreach (var upcoming in upcomingShifts)
            {
                upcoming.Type = ShiftCalendarType.Upcoming;
            }

            foreach (var openShift in openShifts)
            {
                openShift.Type = ShiftCalendarType.Open;
            }

            foreach (var pendingShift in swapRequests)
            {
                pendingShift.Type = ShiftCalendarType.Pending;
            }

            var allShiftCalendar =
                (upcomingShifts)
                .Concat(openShifts)
                .Concat(swapRequests)
                .ToArray();

            var jsonObject = new
            {
                UpcomingShiftsCount = upcomingShifts.Count(),
                OpenShiftsCount = upcomingShifts.Count(),
                PendingShiftsCount = swapRequests.Count(),
                AllShiftsCalendar = allShiftCalendar,
            };

            return this.Ok(jsonObject);
        }
    }
}
