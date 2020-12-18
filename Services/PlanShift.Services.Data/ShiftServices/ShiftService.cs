namespace PlanShift.Services.Data.ShiftServices
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

    public class ShiftService : IShiftService
    {
        private readonly IDeletableEntityRepository<Shift> shiftRepository;

        public ShiftService(IDeletableEntityRepository<Shift> shiftRepository)
        {
            this.shiftRepository = shiftRepository;
        }

        public async Task<string> CreateShiftAsync(string shiftCreatorId, string groupId, DateTime start, DateTime end, string description, decimal bonusPayment = 0)
        {
            var shift = new Shift()
            {
                ShiftCreatorId = shiftCreatorId,
                GroupId = groupId,
                Start = start,
                End = end,
                Description = description,
                BonusPayment = bonusPayment,
                ShiftStatus = ShiftStatus.Open,
            };

            await this.shiftRepository.AddAsync(shift);
            await this.shiftRepository.SaveChangesAsync();

            return shift.Id;
        }

        public async Task ApproveShiftToEmployeeAsync(string id, string employeeId, string managementId)
        {
            var shift = await this.shiftRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            if (shift == null)
            {
                throw new ArgumentException("No such shift found!");
            }

            shift.EmployeeId = employeeId;
            shift.ManagementId = managementId;
            shift.ShiftStatus = ShiftStatus.Approved;

            await this.shiftRepository.SaveChangesAsync();
        }

        public async Task StatusChangeAsync(string id, ShiftStatus newStatus)
        {
            var shift = await this.shiftRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            shift.ShiftStatus = newStatus;
            await this.shiftRepository.SaveChangesAsync();
        }

        public async Task DeleteShift(string id)
        {
            var shift = await this.shiftRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            this.shiftRepository.Delete(shift);

            await this.shiftRepository.SaveChangesAsync();
        }

        public async Task<T> GetShiftByIdAsync<T>(string id)
            => await this.shiftRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<ICollection<T>> GetAllShiftsByGroupAsync<T>(string groupId)
            => await this.shiftRepository
                .AllAsNoTracking()
                .Where(x => x.GroupId == groupId)
                .OrderBy(x => x.Start)
                .To<T>()
                .ToArrayAsync();

        public async Task<IEnumerable<T>> GetPendingShiftsPerGroupAsync<T>(string groupId)
            => await this.shiftRepository
                .AllAsNoTracking()
                .Where(s => s.GroupId == groupId
                            && s.ShiftStatus == ShiftStatus.Pending)
                .To<T>()
                .ToArrayAsync();

        public async Task<IEnumerable<T>> GetUpcomingShiftForUserAsync<T>(string businessId, string userId)
            => await this.shiftRepository
                   .AllAsNoTracking()
                   .Where(s => s.Group.BusinessId == businessId
                               && s.Employee.UserId == userId
                               && s.End > DateTime.UtcNow
                               && s.ShiftStatus == ShiftStatus.Approved)
                   .To<T>()
                   .ToArrayAsync();

        public async Task<IEnumerable<T>> GetOpenShiftsAvailableForUserAsync<T>(string businessId, string userId)
            => await this.shiftRepository
                .AllAsNoTracking()
                .Where(s => s.Group.BusinessId == businessId
                            && s.Group.Employees.Any(e => e.UserId == userId)
                            && s.ShiftStatus == ShiftStatus.Open)

                // TODO: Add achievement check here
                .To<T>()
                .ToArrayAsync();

        public async Task<IEnumerable<T>> GetUsersShiftsWithDeclaredSwapRequestsAsync<T>(string businessId, string userId)
            => await this.shiftRepository
                .AllAsNoTracking()
                .Where(s => s.Group.BusinessId == businessId
                            && s.ShiftStatus == ShiftStatus.Pending
                            && s.ShiftChanges.Any(sc => sc.OriginalEmployee.UserId == userId))
                .To<T>()
                .ToArrayAsync();

        public async Task<IEnumerable<T>> GetTakenShiftsPerUserAsync<T>(string businessId, string userId)
            => await this.shiftRepository
                .AllAsNoTracking()
                .Where(s => s.Group.BusinessId == businessId &&
                            s.ShiftStatus == ShiftStatus.Approved &&
                            s.Employee.UserId != userId)
                .To<T>()
                .ToArrayAsync();
    }
}
