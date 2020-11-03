namespace PlanShift.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ContractTemplate
    {
        public ContractTemplate()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        public byte[] ContractTemplateFile { get; set; }

        [Required]
        public int BusinessId { get; set; }

        public virtual Business Business { get; set; }
    }
}
