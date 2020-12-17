namespace PlanShift.Data.Models
{
    using PlanShift.Data.Common.Models;

    public class GroupEvents : BaseModel<int>
    {
        public string GroupId { get; set; }

        public virtual Group Group { get; set; }

        public string EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}