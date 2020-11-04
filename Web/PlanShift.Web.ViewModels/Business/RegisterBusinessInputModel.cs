namespace PlanShift.Web.ViewModels.Business
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterBusinessInputModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(60)]
        public string BusinessType { get; set; }
    }
}
