﻿namespace PlanShift.Services.Data.ShiftServices
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

        public async Task<string> CreateShift(string shiftCreatorId, string groupId, DateTime start, DateTime end, string description, decimal bonusPayment = 0)
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

        public async Task ApproveShiftToEmployee(string id, string employeeId, string managementId)
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
        public async Task StatusChange(string id, ShiftStatus newStatus)
        {
            var shift = await this.shiftRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            shift.ShiftStatus = newStatus;
            await this.shiftRepository.SaveChangesAsync();
        }

        //public async Task DeleteShift(string id)
        //{
        //    var shift = await this.shiftRepository.All().FirstOrDefaultAsync(x => x.Id == id);

        //    this.shiftRepository.Delete(shift);

        //    await this.shiftRepository.SaveChangesAsync();
        //}

        public async Task<T> GetShiftById<T>(string id)
            => await this.shiftRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<ICollection<T>> GetAllShiftsByGroup<T>(string groupId)
            => await this.shiftRepository
                .AllAsNoTracking()
                .Where(x => x.GroupId == groupId)
                .OrderBy(x => x.Start)
                .To<T>()
                .ToArrayAsync();

        public async Task<ShiftStatus> GetShiftStatus(string id)
            => await this.shiftRepository
                .AllAsNoTracking()
                .Where(s => s.Id == id)
                .Select(s => s.ShiftStatus)
                .FirstOrDefaultAsync();

        public async Task<string> GetGroupIdAsync(string shiftId)
            => await this.shiftRepository
                .All()
                .Where(x => x.Id == shiftId)
                .Select(x => x.GroupId)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> GetPendingShiftsPerGroup<T>(string groupId)
            => await this.shiftRepository
                .AllAsNoTracking()
                .Where(s => s.GroupId == groupId
                            && s.ShiftStatus == ShiftStatus.Pending)
                .To<T>()
                .ToArrayAsync();

        public async Task<IEnumerable<T>> GetUpcomingShiftForUser<T>(string businessId, string userId)
            => await this.shiftRepository
                   .AllAsNoTracking()
                   .Where(s => s.Group.BusinessId == businessId
                               && s.Employee.UserId == userId
                               && s.ShiftStatus == ShiftStatus.Approved)
                   .To<T>()
                   .ToArrayAsync();

        public async Task<IEnumerable<T>> GetOpenShiftsAvailableForUser<T>(string businessId, string userId)
            => await this.shiftRepository
                .AllAsNoTracking()
                .Where(s => s.Group.BusinessId == businessId
                            && s.Group.Employees.Any(e => e.UserId == userId)
                            && s.ShiftStatus == ShiftStatus.Open)
                // TODO: Add achievement check here
                .To<T>()
                .ToArrayAsync();

        public async Task<IEnumerable<T>> GetPendingShiftsPerUser<T>(string businessId, string userId)
            => await this.shiftRepository
                .AllAsNoTracking()
                .Where(s => s.Group.BusinessId == businessId
                            && s.ShiftStatus == ShiftStatus.Pending
                            && s.ShiftChanges.Any(sc => sc.OriginalEmployee.UserId == userId))
                .To<T>()
                .ToArrayAsync();

        public async Task<IEnumerable<T>> GetTakenShiftsPerUser<T>(string businessId, string userId)
            => await this.shiftRepository
                .AllAsNoTracking()
                .Where(s => s.Group.BusinessId == businessId && s.ShiftStatus == ShiftStatus.Approved && s.EmployeeId != userId)
                .To<T>()
                .ToArrayAsync();
    }
}
