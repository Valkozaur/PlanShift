namespace PlanShift.Web.ViewModels.Places
{
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class PlaceInputModel : IMapTo<Place>
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
    }
}