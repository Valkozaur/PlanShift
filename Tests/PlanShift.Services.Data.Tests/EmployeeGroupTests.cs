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

    public class EmployeeGroupTests : DeletableEntityBaseTestClass
    {
        private const decimal Salary = 1000.10M;
        private const string Position = "Test";
        private const string UserId = "Test";
        private const string GroupId = "Test";
        private const string BusinessId = "Test";


        private readonly Mock<IDeletableEntityRepository<EmployeeGroup>> repository;
        private readonly List<EmployeeGroup> fakeDb;
        private IEmployeeGroupService employeeGroupService;

        public EmployeeGroupTests()
        {
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
            var id = await this.employeeGroupService.AddEmployeeToGroupAsync(UserId, GroupId, Salary, Position);

            // Assert
            Assert.NotNull(id);
            Assert.Single(this.fakeDb);
        }

        [Fact]
        public async Task GetAllEmployeesFromGroupShouldReturnAllExistingEmployeesInGroup()
        {
            const int numberOfFakeEmployees = 3;

            // Arrange
            this.fakeDb.Add(new EmployeeGroup() { UserId = UserId, GroupId = GroupId, Salary = Salary, Position = Position });
            this.fakeDb.Add(new EmployeeGroup() { UserId = UserId, GroupId = GroupId, Salary = Salary, Position = Position });
            this.fakeDb.Add(new EmployeeGroup() { UserId = UserId, GroupId = GroupId, Salary = Salary, Position = Position });

            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);

            this.employeeGroupService = new EmployeeGroupService(this.repository.Object);

            // Arrange
            var employees = await this.employeeGroupService.GetAllEmployeesFromGroup<EmployeeGroupIdViewModel>(GroupId);

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
            var employees = await this.employeeGroupService.GetAllEmployeesFromGroup<EmployeeGroupIdViewModel>(GroupId);

            // Assert
            Assert.Empty(employees);
        }

        [Fact]
        public async Task IsEmployeeInGroupShouldReturnTrueIfEmployeeIsInGroup()
        {
            // Arrange
            this.fakeDb.Add(new EmployeeGroup() { UserId = UserId, GroupId = GroupId, Salary = Salary, Position = Position });

            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);
            this.employeeGroupService = new EmployeeGroupService(this.repository.Object);

            // Act
            var isEmployeeInGroup = await this.employeeGroupService.IsEmployeeInGroup(UserId, GroupId);

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
            var isEmployeeInGroup = await this.employeeGroupService.IsEmployeeInGroup(UserId, GroupId);

            // Assert
            Assert.False(isEmployeeInGroup);
        }

        [Fact]
        public async Task IsEmployeeInGroupWithNamesShouldReturnTrueIfEmployeeIs()
        {
            const string groupName1 = "Test";
            const string groupName2 = "Test1";

            // Arrange
            var group1 = new Group(){ Id = GroupId, BusinessId = BusinessId, Name = groupName1 };

            var employeeGroup = new EmployeeGroup
            {
                UserId = UserId,
                GroupId = GroupId,
                Salary = Salary,
                Position = Position,
                Group = group1,
            };

            this.fakeDb.Add(employeeGroup);
            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);

            this.employeeGroupService = new EmployeeGroupService(this.repository.Object);

            // Act
            var isInGroupWithNames = await this.employeeGroupService.IsEmployeeInGroupsWithNames(UserId, BusinessId, groupName1, groupName2);

            // Assert
            Assert.True(isInGroupWithNames);
        }

        [Fact]
        public async Task IsEmployeeInGroupWithNamesShouldReturnFalseIfEmployeeIsNot()
        {
            const string groupName1 = "Test";
            const string groupName2 = "Test1";
            const string groupName3 = "Test3";

            // Arrange

            var group1 = new Group() { Id = GroupId, BusinessId = BusinessId, Name = groupName3 };

            var employeeGroup = new EmployeeGroup
            {
                UserId = UserId,
                GroupId = GroupId,
                Salary = Salary,
                Position = Position,
                Group = group1,
            };

            this.fakeDb.Add(employeeGroup);
            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);

            this.employeeGroupService = new EmployeeGroupService(this.repository.Object);

            // Act
            var isInGroupWithNames = await this.employeeGroupService.IsEmployeeInGroupsWithNames(UserId, BusinessId, groupName1, groupName2);

            // Assert
            Assert.False(isInGroupWithNames);
        }

        [Fact]
        public async Task GetEmployeeIdShouldReturnEmployeeIdIfExists()
        {
            const string employeeId = "Test";

            // Arrange

            this.fakeDb.Add(new EmployeeGroup() { Id = employeeId, UserId = UserId, GroupId = GroupId, Salary = Salary, Position = Position });
            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);

            this.employeeGroupService = new EmployeeGroupService(this.repository.Object);

            // Act
            var getEmployeeId = await this.employeeGroupService.GetEmployeeId(UserId, GroupId);

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
            var getEmployeeId = await this.employeeGroupService.GetEmployeeId(UserId, GroupId);

            // Assert
            Assert.Null(getEmployeeId);
        }
    }
}
