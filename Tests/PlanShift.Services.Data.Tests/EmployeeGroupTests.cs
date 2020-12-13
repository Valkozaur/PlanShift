namespace PlanShift.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Moq;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Web.ViewModels.EmployeeGroup;
    using Xunit;

    public class EmployeeGroupTests : BaseTestClass
    {
        private const decimal Salary = 1000.10M;
        private const string Position = "Test";

        private readonly string userId;
        private readonly string groupId;
        private readonly string businessId;

        private readonly Mock<IDeletableEntityRepository<EmployeeGroup>> repository;
        private readonly List<EmployeeGroup> fakeDb;
        private IEmployeeGroupService employeeGroupService;

        public EmployeeGroupTests()
        : base()
        {
            this.userId = Guid.NewGuid().ToString();
            this.groupId = Guid.NewGuid().ToString();
            this.businessId = Guid.NewGuid().ToString();

            this.repository = new Mock<IDeletableEntityRepository<EmployeeGroup>>();
            this.fakeDb = new List<EmployeeGroup>();
        }

        [Fact]
        public async Task AddEmployeeToGroupAddsCorrectly()
        {
            // Arrange
            this.SetMockedRepositoryCreateOperations(this.repository, this.fakeDb);

            this.employeeGroupService = new EmployeeGroupService(this.repository.Object);

            // Act
            var id = await this.employeeGroupService.AddEmployeeToGroupAsync(this.userId, this.groupId, Salary, Position);

            // Assert
            Assert.NotNull(id);
            Assert.Single(this.fakeDb);
        }

        [Fact]
        public async Task GetAllEmployeesFromGroupShouldReturnAllExistingEmployeesInGroup()
        {
            const int numberOfFakeEmployees = 3;

            // Arrange
            this.fakeDb.Add(new EmployeeGroup() { UserId = this.userId, GroupId = this.groupId, Salary = Salary, Position = Position });
            this.fakeDb.Add(new EmployeeGroup() { UserId = this.userId, GroupId = this.groupId, Salary = Salary, Position = Position });
            this.fakeDb.Add(new EmployeeGroup() { UserId = this.userId, GroupId = this.groupId, Salary = Salary, Position = Position });

            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);

            this.employeeGroupService = new EmployeeGroupService(this.repository.Object);

            // Arrange
            var employees = await this.employeeGroupService.GetAllEmployeesFromGroup<EmployeeGroupIdViewModel>(this.groupId);

            // Assert
            Assert.Equal(numberOfFakeEmployees, employees.Count());
        }

        [Fact]
        public async Task GetAllEmployeesFromGroupShouldReturnNothingIfThereAreNoEmployees()
        {
            // Arrange
            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);

            this.employeeGroupService = new EmployeeGroupService(this.repository.Object);

            // Arrange
            var employees = await this.employeeGroupService.GetAllEmployeesFromGroup<EmployeeGroupIdViewModel>(this.groupId);

            // Assert
            Assert.Empty(employees);
        }

        [Fact]
        public async Task IsEmployeeInGroupShouldReturnTrueIfEmployeeIsInGroup()
        {
            // Arrange
            this.fakeDb.Add(new EmployeeGroup() { UserId = this.userId, GroupId = this.groupId, Salary = Salary, Position = Position });

            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);
            this.employeeGroupService = new EmployeeGroupService(this.repository.Object);

            // Act
            var isEmployeeInGroup = await this.employeeGroupService.IsEmployeeInGroup(this.userId, this.groupId);

            // Assert
            Assert.True(isEmployeeInGroup);
        }

        [Fact]
        public async Task IsEmployeeShouldReturnFalseIfEmplyoeeIsNotInTheGroup()
        {
            // Arrange
            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);
            this.employeeGroupService = new EmployeeGroupService(this.repository.Object);

            // Act
            var isEmployeeInGroup = await this.employeeGroupService.IsEmployeeInGroup(this.userId, this.groupId);

            // Assert
            Assert.False(isEmployeeInGroup);
        }

        [Fact]
        public async Task IsEmployeeInGroupWithNamesShouldReturnTrueIfEmployeeIs()
        {
            // Arrange
            var groupName1 = "Test";
            var groupName2 = "Test1";

            var group1 = new Group(){ Id = this.groupId, BusinessId = this.businessId, Name = groupName1 };

            var employeeGroup = new EmployeeGroup
            {
                UserId = this.userId,
                GroupId = this.groupId,
                Salary = Salary,
                Position = Position,
                Group = group1,
            };

            this.fakeDb.Add(employeeGroup);
            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);

            this.employeeGroupService = new EmployeeGroupService(this.repository.Object);

            // Act
            var isInGroupWithNames = await this.employeeGroupService.IsEmployeeInGroupsWithNames(this.userId, this.businessId, groupName1, groupName2);

            // Assert
            Assert.True(isInGroupWithNames);
        }

        [Fact]
        public async Task IsEmployeeInGroupWithNamesShouldReturnFalseIfEmployeeIsNot()
        {
            // Arrange
            var groupName1 = "Test";
            var groupName2 = "Test1";
            var groupName3 = "Test3";

            var group1 = new Group() { Id = this.groupId, BusinessId = this.businessId, Name = groupName3 };

            var employeeGroup = new EmployeeGroup
            {
                UserId = this.userId,
                GroupId = this.groupId,
                Salary = Salary,
                Position = Position,
                Group = group1,
            };

            this.fakeDb.Add(employeeGroup);
            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);

            this.employeeGroupService = new EmployeeGroupService(this.repository.Object);

            // Act
            var isInGroupWithNames = await this.employeeGroupService.IsEmployeeInGroupsWithNames(this.userId, this.businessId, groupName1, groupName2);

            // Assert
            Assert.False(isInGroupWithNames);
        }

        [Fact]
        public async Task GetEmployeeIdShouldReturnEmployeeIdIfExists()
        {
            // Arrange
            var employeeId = "Test";

            this.fakeDb.Add(new EmployeeGroup() { Id = employeeId, UserId = this.userId, GroupId = this.groupId, Salary = Salary, Position = Position });
            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);

            this.employeeGroupService = new EmployeeGroupService(this.repository.Object);

            // Act
            var getEmployeeId = await this.employeeGroupService.GetEmployeeId(this.userId, this.groupId);

            // Assert
            Assert.Equal(employeeId, getEmployeeId);
        }

        [Fact]
        public async Task GetEmployeeIdShouldReturnNullIfSuchDoNotExist()
        {
            // Arrange
            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);

            this.employeeGroupService = new EmployeeGroupService(this.repository.Object);

            // Act
            var getEmployeeId = await this.employeeGroupService.GetEmployeeId(this.userId, this.groupId);

            // Assert
            Assert.Null(getEmployeeId);
        }
    }
}
