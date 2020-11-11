namespace PlanShift.Services.Data.ShiftApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ShiftApplicationService : IShiftApplicationService
    {
        private readonly IRepository<ShiftApplication> shiftApplicationRepository;

        public ShiftApplicationService(IRepository<ShiftApplication> shiftApplicationRepository)
        {
            this.shiftApplicationRepository = shiftApplicationRepository;
        }

        public async Task<string> CreateShiftApplicationAsync(string shiftId, string employeeId)
        {
            var shiftApplication = new ShiftApplication()
            {
                ShiftId = shiftId,
                EmployeeId = employeeId,
                CreatedOn = DateTime.UtcNow,
            };

            await this.shiftApplicationRepository.AddAsync(shiftApplication);
            await this.shiftApplicationRepository.SaveChangesAsync();

            return shiftApplication.Id;
        }

        public async Task<IEnumerable<T>> GetAllApplicationByShiftIdAsync<T>(string shiftId)
            => await this.shiftApplicationRepository
                .AllAsNoTracking()
                .Where(x => x.ShiftId == shiftId)
                .To<T>()
                .ToArrayAsync();

        public async Task ApproveShiftApplicationAsync(string id)
        {
            var shiftApplication = await this.shiftApplicationRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            shiftApplication.IsApproved = true;
            await this.shiftApplicationRepository.SaveChangesAsync();
        }
    }
}
