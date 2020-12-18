namespace PlanShift.Web.ViewModels.Group
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class GroupChatViewModel : GroupBusinessNamesViewModel, IMapFrom<Group>
    {
        public string Id { get; set; }
    }
}
