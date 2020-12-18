namespace PlanShift.Web.ViewModels.Group
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class GroupBusinessNamesViewModel : IMapFrom<Group>
    {
        public string Name { get; set; }

        public string BusinessName { get; set; }
    }
}
