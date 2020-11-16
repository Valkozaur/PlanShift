﻿namespace PlanShift.Services.Data.ShiftApplication
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
            };

            await this.shiftApplicationRepository.AddAsync(shiftApplication);
            await this.shiftApplicationRepository.SaveChangesAsync();

            return shiftApplication.Id;
        }

        public async Task<bool> HasEmployeeAppliedForShift(string shiftId, string employeeId)
        => await this.shiftApplicationRepository
            .AllAsNoTracking()
            .AnyAsync(x => x.ShiftId == shiftId && x.EmployeeId == employeeId);

        public async Task<IEnumerable<T>> GetAllApplicationByShiftIdAsync<T>(string shiftId)
            => await this.shiftApplicationRepository
                .AllAsNoTracking()
                .Where(x => x.ShiftId == shiftId)
                .To<T>()
                .ToArrayAsync();

        public async Task ApproveShiftApplicationAsync(string id)
        {
            var shiftApplication = await this.shiftApplicationRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            shiftApplication.Status = ShiftApplicationStatus.Approved;
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

        public async Task<IEnumerable<T>> GetAllActiveShiftApplicationsPerGroup<T>(string groupId)
            => await this.shiftApplicationRepository
                .All()
                .Where(x =>
                        x.Shift.GroupId == groupId
                        && x.Status == ShiftApplicationStatus.Pending
                        && x.Shift.End < DateTime.UtcNow)
                .To<T>()
                .ToArrayAsync();

        public Task<IEnumerable<T>> GetAllActiveShiftApplicationsPerBusiness<T>(string businessId)
        {
            throw new NotImplementedException();
        }
    }
}
