namespace PlanShift.Services.Data.ShiftApplicationServices
{
    using System;
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

        public async Task<bool> HasEmployeeActiveApplicationForShiftAsync(string shiftId, string employeeId)
        => await this.shiftApplicationRepository
            .AllAsNoTracking()
            .AnyAsync(sa
                => sa.ShiftId == shiftId &&
                   sa.EmployeeId == employeeId &&
                   sa.Status == ShiftApplicationStatus.Pending);

        public async Task ApproveShiftApplicationAsync(string id)
        {
            var shiftApplication = await this.shiftApplicationRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (shiftApplication == null)
            {
                throw new ArgumentException("Shift application with this id does not exist!");
            }

            shiftApplication.Status = ShiftApplicationStatus.Approved;

            await this.shiftApplicationRepository.SaveChangesAsync();
        }

        public async Task DeclineAllShiftApplicationsPerShiftAsync(string shiftId)
        {
            var shiftApplications = await this.shiftApplicationRepository
                .All()
                .Where(sa => sa.ShiftId == shiftId)
                .ToArrayAsync();

            if (shiftApplications.Length == 0)
            {
                throw new ArgumentException("There are no shift applications for this shift!");
            }

            foreach (var shiftApplication in shiftApplications)
            {
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

        public Task<int> GetCountOfPendingApplicationsByBusinessIdAsync(string businessId)
            => this.shiftApplicationRepository
            .AllAsNoTracking()
            .CountAsync(x
                => x.Shift.Group.BusinessId == businessId
                && x.Status == ShiftApplicationStatus.Pending);
    }
}
