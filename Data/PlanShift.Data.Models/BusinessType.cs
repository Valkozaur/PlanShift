namespace PlanShift.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Data.Common.Models;

    public class BusinessType : BaseModel<int>
    {

        public BusinessType()
        {
            this.Businesses = new HashSet<Business>();
        }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        public ICollection<Business> Businesses { get; set; }
    }
}
