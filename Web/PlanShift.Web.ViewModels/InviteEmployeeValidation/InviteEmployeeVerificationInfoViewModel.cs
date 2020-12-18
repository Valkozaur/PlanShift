namespace PlanShift.Web.ViewModels.InviteEmployeeValidation
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class InviteEmployeeVerificationInfoViewModel : InviteEmployeeVerificationEmailViewModel, IMapFrom<InviteEmployeeVerification>
    {
        public string GroupId { get; set; }

        public string Position { get; set; }

        public decimal Salary { get; set; }
    }
}
