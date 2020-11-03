namespace PlanShift.Services.Data.UserServices
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<PlanShiftUser> userRepository;

        public UserService(IDeletableEntityRepository<PlanShiftUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<string> CreateUser(string name, string password, string email)
        {
            var user = new PlanShiftUser()
            {
                UserName = name,
                PasswordHash = password,
                Email = email,
            };

            await this.userRepository.AddAsync(user);
            await this.userRepository.SaveChangesAsync();

            return user.Id;
        }

        public async Task<TViewModel> GetUser<TViewModel>(string id)
            => await this.userRepository
                .All()
                .Where(x => x.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

        public async Task UpdateUser(string id, string name = null, string password = null, string email = null)
        {
            var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            var isUpdated = false;

            if (user != null && (name != null || password != null || email != null))
            {
                user.UserName = name ?? user.UserName;
                user.PasswordHash = password ?? user.PasswordHash;
                user.Email = email ?? user.Email;

                isUpdated = true;
            }

            if (isUpdated)
            {
                user.ModifiedOn = DateTime.UtcNow;
                await this.userRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteUser(string id)
        {
            var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            this.userRepository.Delete(user);
            await this.userRepository.SaveChangesAsync();
        }
    }
}
