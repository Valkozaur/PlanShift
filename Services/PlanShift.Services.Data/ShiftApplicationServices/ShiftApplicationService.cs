namespace PlanShift.Services.Data.ShiftApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Data.Models.Enumerations;
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
                Status = ShiftApplicationStatus.Pending,
            };

            await this.shiftApplicationRepository.AddAsync(shiftApplication);
            await this.shiftApplicationRepository.SaveChangesAsync();

            return shiftApplication.Id;
        }

        public async Task<bool> HasEmployeeAppliedForShift(string shiftId, string employeeId)
        => await this.shiftApplicationRepository
            .AllAsNoTracking()
            .AnyAsync(x => x.ShiftId == shiftId && x.EmployeeId == employeeId);

        public async Task ApproveShiftApplicationAsync(string id)
        {
            var shiftApplications = await this.shiftApplicationRepository
                .All()
                .Where(x => x.Id == id)
                .ToListAsync();

            foreach (var shiftApplication in shiftApplications)
            {
                if (shiftApplication.Id == id)
                {
                    shiftApplication.Status = ShiftApplicationStatus.Approved;
                    continue;
                }

                shiftApplication.Status = ShiftApplicationStatus.Declined;
            }

            await this.shiftApplicationRepository.SaveChangesAsync();
        }

        public async Task<T> GetShiftApplicationById<T>(string id)
            => await this.shiftApplicationRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public Task<int> GetCountByBusinessIdAsync(string businessId)
            => this.shiftApplicationRepository
            .All()
            .CountAsync(x
                => x.Shift.Group.BusinessId == businessId
                && x.Status == ShiftApplicationStatus.Pending);
    }
}
