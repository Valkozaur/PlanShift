using PlanShift.Services.Data.GroupServices;
using PlanShift.Web.ViewModels.EmployeeGroup;
using PlanShift.Web.ViewModels.Group;

namespace PlanShift.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using PlanShift.Common;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.EventsServices;
    using PlanShift.Services.Data.PlacesServices;
    using PlanShift.Web.Tools.ActionFilters;
    using PlanShift.Web.Tools.SessionExtension;
    using PlanShift.Web.ViewModels.Events;

    public class EventController : BaseController
    {
        private readonly IEventService eventService;
        private readonly IGroupService groupService;
        private readonly IPlaceService placeService;
        private readonly IEmployeeGroupService employeeGroupService;

        public EventController(
            IEventService eventService,
            IGroupService groupService,
            IPlaceService placeService,
            IEmployeeGroupService employeeGroupService)
        {
            this.eventService = eventService;
            this.groupService = groupService;
            this.placeService = placeService;
            this.employeeGroupService = employeeGroupService;
        }

        [SessionValidation(GlobalConstants.BusinessIdSessionName)]
        public async Task<IActionResult> Index(string id)
        {
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessIdSessionName);

            var groups = this.groupService.GetGroupWhichDoNotParticipateInTheEventByBusinessAsync<GroupPeopleCountViewModel>(businessId, id);

            var @event = await this.eventService.GetEventById<EventFullInfoViewModel>(id);
            
            var viewModel = new
            {
                EventInfo = @event,
                Groups = groups,
            }

            return this.View(viewModel);
        }

        [SessionValidation(GlobalConstants.BusinessIdSessionName)]
        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.HrGroupName } })]
        public async Task<IActionResult> Create(int? id)
        {
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessIdSessionName);
            var places = await this.placeService.GetAllPlacesByBusinessAsync<PlaceInfoViewModel>(businessId);

            var viewModel = new EventInputModel()
            {
                PlaceId = id ?? default,
                Places = places,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [SessionValidation(GlobalConstants.BusinessIdSessionName)]
        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.HrGroupName } })]
        public async Task<IActionResult> Create(EventInputModel eventInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(eventInput);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessIdSessionName);

            var employee =
                await this.employeeGroupService.GetEmployeeIdByBusinessNameAndGroupNamesAsync<EmployeeIdGroupIdModel>(
                    userId,
                    businessId,
                    GlobalConstants.AdminsGroupName,
                    GlobalConstants.HrGroupName);

            await this.eventService.CreateAsync(
                eventInput.Name,
                eventInput.PlaceId,
                eventInput.Start,
                eventInput.End,
                eventInput.Description,
                employee.GroupId,
                employee.Id);

            return this.RedirectToAction("Index", "Business");
        }

        [SessionValidation(GlobalConstants.BusinessIdSessionName)]
        [TypeFilter(typeof(IsEmployeeInRoleGroupAttribute), Arguments = new object[] { new[] { GlobalConstants.AdminsGroupName, GlobalConstants.HrGroupName } })]
        public async Task<IActionResult> All()
        {
            var businessId = await this.HttpContext.Session.GetStringAsync(GlobalConstants.BusinessIdSessionName);

            var events = await this.eventService.GetAllEventsPerBusiness<EventBasicInfoViewModel>(businessId);

            var viewModel = new EventListViewModel<EventBasicInfoViewModel>()
            {
                Events = events,
            };

            return this.View(viewModel);
        }
    }
}
