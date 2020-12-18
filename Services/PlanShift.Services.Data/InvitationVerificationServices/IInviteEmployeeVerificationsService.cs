namespace PlanShift.Services.Data.InvitationVerificationServices
{
    using System.Threading.Tasks;

    public interface IInviteEmployeeVerificationsService
    {
        Task<string> CreateShiftVerificationAsync(string groupId, string email, string position, decimal salary);

        Task<bool> IsVerificationValid(string id);

        Task<T> GetVerificationAsync<T>(string id);
    }
}
