﻿namespace PlanShift.Web.Controllers
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Services.Data.InvitationVerificationServices;
    using PlanShift.Services.Messaging;
    using PlanShift.Web.ViewModels.EmployeeGroup;
    using PlanShift.Web.ViewModels.Group;

    public class InviteUserController : BaseController
    {
        private const string EmailSubject = "Become part of PlanShift!";

        private readonly IGroupService groupService;
        private readonly IInviteEmployeeVerificationsService inviteEmployeeVerificationsService;
        private readonly IEmailSender emailSender;

        public InviteUserController(
            IGroupService groupService,
            IInviteEmployeeVerificationsService inviteEmployeeVerificationsService,
            IEmailSender emailSender
            )
        {
            this.groupService = groupService;
            this.inviteEmployeeVerificationsService = inviteEmployeeVerificationsService;

            this.emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> InviteUserToGroup(EmployeeToGroupInvitationInputModel details)
        {
            var groupNames = await this.groupService.GetGroupAsync<GroupBusinessNamesViewModel>(details.GroupId);

            var html = new StringBuilder();

            var guid = Guid.NewGuid().ToString();

            await this.inviteEmployeeVerificationsService.CreateShiftVerificationAsync(guid, details.GroupId, details.Email, details.Position, details.Salary);

            html.AppendLine($"<h1> {groupNames.BusinessName} wants to invite you to {GlobalConstants.SystemName}</h1>");
            html.AppendLine($"<p> You are invited to become part of the group {groupNames.Name} at position {details.Position}</p>");
            html.AppendLine($"<p> Your salary will be {details.Salary}</p>");
            html.AppendLine($"<h3> If you accept you will become a member of {GlobalConstants.SystemName}, please click the button below! </h3>");
            html.AppendLine("<a> https://localhost:44319/InviteUser/AcceptInvitation/" + guid + "</a>");

            await this.emailSender.SendEmailAsync(GlobalConstants.EmailAddress, groupNames.BusinessName, details.Email, EmailSubject, html.ToString());

            return this.RedirectToAction("Index", "People", new { ActiveTabGroupId = details.GroupId });
        }

        public async Task<IActionResult> AcceptInvitation(string id)
        {
            var isInvitationValid = await this.inviteEmployeeVerificationsService.IsVerificationValidAsync(id);

            if (!isInvitationValid)
            {
                return this.View("ErrorView");
            }

            return this.LocalRedirect($"/Identity/Account/Register?validationId={id}");
        }
    }
}
