namespace PlanShift.Web.ViewModels.Business
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Web.ViewModels.BusinessType;

    public class BusinessRegisterInputModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int BusinessTypeId { get; set; }

        public IEnumerable<BusinessTypeDropDownViewModel> BusinessTypes { get; set; }
    }
}
