namespace PlanShift.Services.Data.ShiftChangeServices
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

    public class ShiftChangeService : IShiftChangeService
    {
        private readonly IRepository<ShiftChange> shiftChangeRepository;

        public ShiftChangeService(IRepository<ShiftChange> shiftChangeRepository)
        {
            this.shiftChangeRepository = shiftChangeRepository;
        }

        public async Task<string> CreateShiftChangeAsync(string shiftId, string originalEmployeeId, string candidateEmployeeId)
        {
            var shiftChange = new ShiftChange()
            {
                ShiftId = shiftId,
                OriginalEmployeeId = originalEmployeeId,
                PendingEmployeeId = candidateEmployeeId,
                Status = ShiftApplicationStatus.Pending,
            };

            await this.shiftChangeRepository.AddAsync(shiftChange);
            await this.shiftChangeRepository.SaveChangesAsync();

            return shiftChange.Id;
        }

        public async Task<T> GetShiftChangeById<T>(string id)
            => await this.shiftChangeRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> GetShiftChangesPerShift<T>(string shiftId)
        {
            return await this.shiftChangeRepository
                .AllAsNoTracking()
                .Where(sc => sc.ShiftId == shiftId && sc.Status == ShiftApplicationStatus.Pending)
                .OrderBy(sc => sc.Shift.Group.Name)
                .ThenByDescending(sc => sc.Shift.Start)
                .To<T>()
                .ToArrayAsync();
        }

        public async Task ProcessShiftChangeOriginalEmployeeStatus(string userId, string shiftChangeId, bool isAccepted)
        {
            var shiftChange = await this.shiftChangeRepository
                .All()
                .FirstOrDefaultAsync(sc => sc.Id == shiftChangeId && sc.OriginalEmployee.UserId == userId);

            if (shiftChange == null)
            {
                throw new ArgumentException("No such shiftChange found!");
            }

            shiftChange.IsApprovedByOriginalEmployee = isAccepted;
            await this.shiftChangeRepository.SaveChangesAsync();
        }

        public async Task ApproveShiftChange(string shiftChangeId, string managerId)
        {
            var shiftChange = await this.shiftChangeRepository.All().FirstOrDefaultAsync(x => x.Id == shiftChangeId);

            if (shiftChange == null)
            {
                throw new ArgumentNullException();
            }

            shiftChange.ManagementId = managerId;
            shiftChange.Status = ShiftApplicationStatus.Approved;

            await this.shiftChangeRepository.SaveChangesAsync();
        }

        public async Task DeclineShiftChange(string shiftChangeId, string managerId)
        {
            var shiftChange = await this.shiftChangeRepository.All().FirstOrDefaultAsync(x => x.Id == shiftChangeId);

            shiftChange.ManagementId = managerId;
            shiftChange.Shift.ShiftStatus = ShiftStatus.Approved;

            await this.shiftChangeRepository.SaveChangesAsync();
        }

        public Task<int> GetCountByBusinessIdAsync(string businessId)
            => this.shiftChangeRepository
                .All()
                .CountAsync(x =>
                x.Shift.Group.BusinessId == businessId
                && x.Status == ShiftApplicationStatus.Pending
                && x.IsApprovedByOriginalEmployee == true);

        public async Task<IEnumerable<T>> GetShiftChangesPerGroupAsync<T>(string groupId, ShiftApplicationStatus shiftApplicationStatus = ShiftApplicationStatus.Pending)
        => await this.shiftChangeRepository
            .AllAsNoTracking()
            .Where(sc => sc.Shift.GroupId == groupId && sc.Status == shiftApplicationStatus && sc.IsApprovedByOriginalEmployee == true)
            .To<T>()
            .ToArrayAsync();
    }
}
