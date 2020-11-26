﻿using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace PlanShift.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Data.Migrations;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Web.ViewModels.EmployeeGroup;
    using PlanShift.Web.ViewModels.Shift;

    public class ShiftController : Controller
    {
        private readonly IShiftService shiftService;
        private readonly IEmployeeGroupService employeeGroupService;
        private readonly IGroupService groupService;
        private readonly UserManager<PlanShiftUser> userManager;

        public ShiftController(IShiftService shiftService, IEmployeeGroupService employeeGroupService, IGroupService groupService, UserManager<PlanShiftUser> userManager)
        {
            this.shiftService = shiftService;
            this.employeeGroupService = employeeGroupService;
            this.groupService = groupService;
            this.userManager = userManager;
        }

        public IActionResult Create(string groupId)
        {
            var model = new CreateShiftInputModel()
            {
                GroupId = groupId,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateShiftInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.userManager.GetUserId(this.User);

            var employeeGroup = await this.employeeGroupService.GetEmployeeGroupById<EmployeeGroupIsManagementInfo>(userId, input.GroupId);

            if (employeeGroup.IsManagement)
            {
                await this.shiftService.CreateShift(employeeGroup.Id, input.GroupId, input.Start, input.End, input.Description, input.BonusPayment ?? 0);
            }
            else
            {
                // TODO: Return error;
            }

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<JsonResult> GetGroupShifts(string groupId)
        {
            var shifts = await this.shiftService.GetAllShiftsByGroup<ShiftCalendarViewModel>(groupId);
            var groupName = await this.groupService.GetGroupName(groupId);

            var viewModel = new ShiftListViewModel()
            {
                Shifts = shifts.ToArray(),
            };

            return this.Json(viewModel);
        }
    }
}