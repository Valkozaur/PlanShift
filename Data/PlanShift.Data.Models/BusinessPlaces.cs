namespace PlanShift.Data.Models
{
    using PlanShift.Data.Common.Models;

    public class BusinessPlaces : BaseModel<int>
    {
        public string BusinessId { get; set; }

        public virtual Business Business { get; set; }

        public int PlaceId { get; set; }

        public virtual Place Place { get; set; }
    }
}