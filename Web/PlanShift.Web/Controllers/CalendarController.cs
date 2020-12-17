namespace PlanShift.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Services.Data.ShiftApplicationServices;
    using PlanShift.Services.Data.ShiftChangeServices;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.Tools.ActionFilters;
    using PlanShift.Web.Tools.SessionExtension;
    using PlanShift.Web.ViewModels.Enumerations;
    using PlanShift.Web.ViewModels.Shift;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController : BaseController
    {
        private readonly IShiftService shiftService;

        public CalendarController(
            IShiftService shiftService)
        {
            this.shiftService = shiftService;
        }

        [HttpGet]
        [SessionValidation(GlobalConstants.BusinessIdSessionName)]
        public async Task<ActionResult> Get()
        {
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessIdSessionName);

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var upcomingShifts = await this.shiftService.GetUpcomingShiftForUserAsync<ShiftCalendarViewModel>(businessId, userId);
            var openShifts = await this.shiftService.GetOpenShiftsAvailableForUserAsync<ShiftCalendarViewModel>(businessId, userId);
            var swapRequests = await this.shiftService.GetUsersShiftsWithDeclaredSwapRequestsAsync<ShiftCalendarViewModel>(businessId, userId);
            var takenShifts = await this.shiftService.GetTakenShiftsPerUserAsync<ShiftCalendarViewModel>(businessId, userId);

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

            foreach (var takenShift in takenShifts)
            {
                takenShift.Type = ShiftCalendarType.Taken;
            }

            var allShiftCalendar =
                (upcomingShifts)
                .Concat(openShifts)
                .Concat(swapRequests)
                .Concat(takenShifts)
                .ToArray();

            var jsonObject = new
            {
                UpcomingShiftsCount = upcomingShifts.Count(),
                OpenShiftsCount = openShifts.Count(),
                PendingShiftsCount = swapRequests.Count(),
                TakenShifts = swapRequests.Count(),
                AllShiftsCalendar = allShiftCalendar,
            };

            return this.Ok(jsonObject);
        }

        [HttpGet("GetGroupShifts")]
        [SessionValidation(GlobalConstants.BusinessIdSessionName)]
        public async Task<ActionResult> GetGroupShifts(string groupId)
        {
            var shifts = await this.shiftService.GetAllShiftsByGroupAsync<ShiftCalendarViewModel>(groupId);

            var jsonObject = new ShiftListViewModel()
            {
                ShiftCount = shifts.Count,
                Shifts = shifts.ToArray(),
            };

            return this.Ok(jsonObject);
        }
    }
}
