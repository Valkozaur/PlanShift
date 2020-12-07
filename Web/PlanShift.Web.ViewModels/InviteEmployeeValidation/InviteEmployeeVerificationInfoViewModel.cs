namespace PlanShift.Web.ViewModels.InviteEmployeeValidation
{
    using PlanShift.Services.Mapping;
    using PlanShift.Data.Models;

    public class InviteEmployeeVerificationInfoViewModel : InviteEmployeeVerificationEmailViewModel, IMapFrom<InviteEmployeeVerifications>
    {
        public string GroupId { get; set; }

        public string Position { get; set; }

        public decimal Salary { get; set; }
    }
}