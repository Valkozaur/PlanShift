namespace PlanShift.Web.Hubs
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using PlanShift.Common;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Web.Tools.ActionFilters;
    using PlanShift.Web.Tools.SessionExtension;

    [Authorize]
    public class TestChatHub : Hub<ITestChatHub>
    {
        private readonly IEmployeeGroupService employeeGroupService;

        public TestChatHub(IEmployeeGroupService employeeGroupService)
        {
            this.employeeGroupService = employeeGroupService;
        }

        private string Username => this.Context.User.Identity.Name;

        public override async Task OnConnectedAsync()
        {
        }

        // public override async Task OnDisconnectedAsync(Exception ex)
        //    => await this.Clients.Group(this.groupName).UserLoggedOff(
        //        new
        //        {
        //            user = this.Username,
        //        });

        public async Task PostMessage(string message, string groupName)
            => await this.Clients.Group(groupName).MessageReceived(new Message() { User = this.Username, Text = message });

        // public async Task UserTyping(bool isTyping)
        //    => await this.Clients.OthersInGroup(this.groupName).UserTyping(
        //        new
        //        {
        //            isTyping,
        //            user = this.Username,
        //        });

        public async Task JoinGroup(string groupId)
        {
            var userId = this.Context.UserIdentifier;

            var isEmployeeParticipantInGroup = await this.employeeGroupService.IsEmployeeInGroup(userId, groupId);

            if (isEmployeeParticipantInGroup)
            {
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupId);

                await this.Clients.Caller.MessageReceived(new Message() { User = this.Username, Text = $"💯 Hi, {this.Username}! This chat application is powered by SignalR 👍🏽" });

                await this.Clients.OthersInGroup(groupId).UserLoggedOn(
                    new
                    {
                        user = this.Username,
                    });

                var groups = this.Groups;
            }
            else
            {
                this.Context.Abort();
            }
        }
    }
}
