namespace PlanShift.Web.ViewModels.InviteEmployeeValidation
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class InviteEmployeeVerificationEmailViewModel : IMapFrom<InviteEmployeeVerification>
    {
        public string Email { get; set; }
    }
}