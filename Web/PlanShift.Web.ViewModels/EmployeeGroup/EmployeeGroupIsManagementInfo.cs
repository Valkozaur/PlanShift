namespace PlanShift.Web.ViewModels.EmployeeGroup
{
    using AutoMapper;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class EmployeeGroupIsManagementInfo : IMapFrom<EmployeeGroup>
    {
        public string Id { get; set; }
    }
}
