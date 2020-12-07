namespace PlanShift.Web.ViewModels.InviteEmployeeValidation
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class InviteEmployeeVerificationEmailViewModel : IMapFrom<InviteEmployeeVerifications>
    {
        public string Email { get; set; }
    }
}