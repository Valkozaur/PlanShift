namespace PlanShift.Web.ViewModels.Group
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class GroupAllViewModel : IMapFrom<Group>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int EmployeesCount { get; set; }

        public int ShiftsCount { get; set; }
    }
}
