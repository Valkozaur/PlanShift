namespace PlanShift.Web.Views.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PlanShift.Common;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Web.Tools.ActionFilters;
    using PlanShift.Web.ViewModels.People;

    public class GroupMembersViewComponent : ViewComponent
    {
        private readonly IEmployeeGroupService employeeGroupService;

        public GroupMembersViewComponent(IEmployeeGroupService employeeGroupService)
        {
            this.employeeGroupService = employeeGroupService;
        }

        [GetSessionInformation(GlobalConstants.BusinessIdSessionName)]
        public async Task<IViewComponentResult> InvokeAsync(string groupId)
        {
            var employees = await this.employeeGroupService.GetAllEmployeesFromGroup<EmployeeViewModel>(groupId);

            var viewModel = new EmployeeListViewModel<EmployeeViewModel>()
            {
                Employees = employees,
            };

            return this.View(viewModel);
        }
    }
}
