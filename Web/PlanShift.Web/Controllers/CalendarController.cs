﻿namespace PlanShift.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Services.Data.ShiftApplication;
    using PlanShift.Services.Data.ShiftChangeServices;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.Tools.SessionExtension;
    using PlanShift.Web.ViewModels.Enumerations;
    using PlanShift.Web.ViewModels.Shift;

    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController : BaseController
    {
        private readonly IShiftService shiftService;
        private readonly IShiftChangeService shiftChangeService;
        private readonly IShiftApplicationService shiftApplicationService;

        public CalendarController(
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
        public async Task<ActionResult> Get()
        {
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessSessionName);

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

        [Authorize]
        [HttpGet("GetGroupShifts")]
        public async Task<ActionResult> GetGroupShifts(string groupId)
        {
            var shifts = await this.shiftService.GetAllShiftsByGroup<ShiftCalendarViewModel>(groupId);

            var jsonObject = new ShiftListViewModel()
            {
                ShiftCount = shifts.Count,
                Shifts = shifts.ToArray(),
            };

            return this.Ok(jsonObject);
        }
    }
}