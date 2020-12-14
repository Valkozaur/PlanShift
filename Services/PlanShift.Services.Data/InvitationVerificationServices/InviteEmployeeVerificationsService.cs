namespace PlanShift.Services.Data.InvitationVerificationServices
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class InviteEmployeeVerificationsService : IInviteEmployeeVerificationsService
    {
        private readonly IRepository<InviteEmployeeVerification> verificationRepository;

        public InviteEmployeeVerificationsService(IRepository<InviteEmployeeVerification> verificationRepository)
        {
            this.verificationRepository = verificationRepository;
        }

        public async Task<string> CreateShiftVerificationAsync(string groupId, string email, string position, decimal salary)
        {
            var currentShiftVerifications = await this.verificationRepository.All().Where(x => x.Email == email).ToArrayAsync();

            foreach (var shiftVerification in currentShiftVerifications)
            {
                shiftVerification.Used = true;
            }

            var userInvitationVerification = new InviteEmployeeVerification()
            {
                Id = Guid.NewGuid().ToString(),
                GroupId = groupId,
                Email = email,
                Position = position,
                Salary = salary,
            };

            await this.verificationRepository.AddAsync(userInvitationVerification);
            await this.verificationRepository.SaveChangesAsync();

            return userInvitationVerification.Id;
        }

        public async Task<bool> IsVerificationValid(string id)
        {
            var verification = await this.verificationRepository
                .All()
                .FirstOrDefaultAsync(ivs => ivs.Id == id && !ivs.Used && ivs.CreatedOn < ivs.CreatedOn.AddDays(1));

            if (verification == null)
            {
                return false;
            }

            verification.Used = true;
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