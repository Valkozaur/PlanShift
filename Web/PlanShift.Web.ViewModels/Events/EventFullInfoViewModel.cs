namespace PlanShift.Web.ViewModels.Events
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class EventFullInfoViewModel : EventBasicInfoViewModel, IMapFrom<Event>
    {
        public string PlaceName { get; set; }

        public string CreatorUserFullName { get; set; }

        public string Description { get; set; }

        public int ParticipantsCount { get; set; }
    }
}
