namespace PlanShift.Services.Data.InvitationVerificationServices
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class InviteEmployeeVerificationsService : IInviteEmployeeVerificationsService
    {
        private readonly IRepository<InviteEmployeeVerifications> verificationRepository;

        public InviteEmployeeVerificationsService(IRepository<InviteEmployeeVerifications> verificationRepository)
        {
            this.verificationRepository = verificationRepository;
        }

        public async Task CreateShiftVerificationAsync(string guidId, string groupId, string email, string position, decimal salary)
        {
            var userInvitationVerification = new InviteEmployeeVerifications()
            {
                Id = guidId,
                GroupId = groupId,
                Email = email,
                Position = position,
                Salary = salary,
            };

            await this.verificationRepository.AddAsync(userInvitationVerification);
            await this.verificationRepository.SaveChangesAsync();
        }

        public async Task<bool> IsVerificationValidAsync(string guidId)
        {
            var verification = await this.verificationRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(ivs => ivs.Id == guidId && !ivs.Used && ivs.CreatedOn < ivs.CreatedOn.AddDays(1));

            if (verification == null)
            {
                return false;
            }

            //TODO: UNCOMMENT!
            //verification.Used = true;
            await this.verificationRepository.SaveChangesAsync();
            return true;
        }

        public async Task<T> GetVerificationAsync<T>(string id)
            => await this.verificationRepository
                .AllAsNoTracking()
                .Where(ivs => ivs.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
    }
}