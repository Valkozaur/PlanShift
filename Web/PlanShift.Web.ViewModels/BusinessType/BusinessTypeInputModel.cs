namespace PlanShift.Web.ViewModels.BusinessType
{
    using System.ComponentModel.DataAnnotations;

    public class BusinessTypeInputModel
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
    }
}
