namespace PlanShift.Web.ViewModels.Group
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class GroupPeopleCountViewModel : IMapFrom<Group>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int EmployeesCount { get; set; }
    }
}
