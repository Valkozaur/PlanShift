﻿namespace PlanShift.Services.Data.BusinessServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class BusinessService : IBusinessService
    {
        private readonly IDeletableEntityRepository<Business> businessRepository;

        public BusinessService(IDeletableEntityRepository<Business> businessRepository)
        {
            this.businessRepository = businessRepository;
        }

        public async Task<string> CreateBusinessAsync(string ownerId, string name, int typeId)
        {
            var business = new Business()
            {
                OwnerId = ownerId,
                Name = name,
                BusinessTypeId = typeId,
            };

            var adminGroup = new Group()
            {
                Name = "Admins",
                StandardSalary = 0,
            };

            adminGroup.Employees.Add(new EmployeeGroup() { UserId = ownerId, Position = "Owner", Salary = 0 });

            var hrGroup = new Group()
            {
                Name = "HR Managers",
                StandardSalary = 0,
            };

            hrGroup.Employees.Add(new EmployeeGroup() { UserId = ownerId, Position = "Owner", Salary = 0});

            var scheduleGroup = new Group()
            {
                Name = "Schedule Managers",
                StandardSalary = 0,
            };

            scheduleGroup.Employees.Add(new EmployeeGroup() { UserId = ownerId, Position = "Owner", Salary = 0});

            business.Groups.Add(adminGroup);
            business.Groups.Add(hrGroup);
            business.Groups.Add(scheduleGroup);

            await this.businessRepository.AddAsync(business);
            await this.businessRepository.SaveChangesAsync();

            return business.Id;
        }

        public async Task<bool> IsEmployeeAnOwner(string businessId, string employeeId)
            => await this.businessRepository
                .AllAsNoTracking()
                .AnyAsync(b => b.Id == businessId &&
                               b.OwnerId == b.Groups
                                   .SelectMany(g => g.Employees)
                                   .FirstOrDefault(e => e.Id == employeeId)
                                   .UserId);

        // public async Task<string> UpdateBusinessAsync(string businessId, string ownerId = null, string name = null, int? typeId = null)
        // {
        //    var business = await this.businessRepository.All().FirstOrDefaultAsync(x => x.Id == businessId);

        // if (business != null && (ownerId != null || name != null || typeId != null))
        //    {
        //        business.Name = name ?? business.Name;
        //        business.OwnerId = ownerId ?? business.OwnerId;
        //        business.BusinessTypeId = typeId ?? business.BusinessTypeId;
        //    }

        // await this.businessRepository.SaveChangesAsync();

        // return business?.Id;
        // }
        public async Task<IEnumerable<T>> GetAllForUserAsync<T>(string userId, int count = 0)
        {
            var query = this.businessRepository
                .AllAsNoTracking()
                .Where(x => x.OwnerId == userId || x.Groups.Any(g => g.Employees.Any(e => e.UserId == userId)));

            if (count != 0)
            {
                query = query.Take(count);
            }

            return await query.To<T>().ToArrayAsync();
        }

        public async Task<T> GetBusinessAsync<T>(string id)
            => await this.businessRepository
                .AllAsNoTracking()
                .Where(b => b.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<string> GetOwnerIdAsync(string id)
            => await this.businessRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => x.OwnerId)
                .FirstOrDefaultAsync();
    }
}
