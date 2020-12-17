namespace PlanShift.Services.Data.Tests
{

    using System.Linq;
    using System.Threading.Tasks;
    using PlanShift.Data.Models;

    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.Tests.BaseTestClasses;
    using PlanShift.Web.ViewModels.EmployeeGroup;

    using Xunit;

    public class EmployeeGroupTests : DeletableEntityTestClass<EmployeeGroup>
    {
        private const decimal Salary = 1000.10M;
        private const string Position = "Test";
        private const string UserId = "Test";
        private const string GroupId = "Test";
        private const string BusinessId = "Test";

        private IEmployeeGroupService employeeGroupService;

        [Fact]
        public async Task AddEmployeeToGroupAddsCorrectly()
        {
            // Arrange
            this.employeeGroupService = new EmployeeGroupService(this.GetMockedRepositoryWithCreateOperations());

            // Act
            var id = await this.employeeGroupService.AddEmployeeToGroupAsync(UserId, GroupId, Salary, Position);

            // Assert
            Assert.NotNull(id);
            Assert.Single(this.FakeDb);
        }

        [Fact]
        public async Task GetAllEmployeesFromGroupShouldReturnAllExistingEmployeesInGroup()
        {
            const int numberOfFakeEmployees = 3;

            // Arrange
            this.FakeDb.Add(new EmployeeGroup() { UserId = UserId, GroupId = GroupId, Salary = Salary, Position = Position });
            this.FakeDb.Add(new EmployeeGroup() { UserId = UserId, GroupId = GroupId, Salary = Salary, Position = Position });
            this.FakeDb.Add(new EmployeeGroup() { UserId = UserId, GroupId = GroupId, Salary = Salary, Position = Position });

            this.employeeGroupService = new EmployeeGroupService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Arrange
            var employees = await this.employeeGroupService.GetAllEmployeesFromGroup<EmployeeGroupIdViewModel>(GroupId);

            // Assert
            Assert.Equal(numberOfFakeEmployees, employees.Count());
        }

        [Fact]
        public async Task GetAllEmployeesFromGroupShouldReturnNothingIfThereAreNoEmployees()
        {
            // Arrange
            this.employeeGroupService = new EmployeeGroupService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Arrange
            var employees = await this.employeeGroupService.GetAllEmployeesFromGroup<EmployeeGroupIdViewModel>(GroupId);

            // Assert
            Assert.Empty(employees);
        }

        [Fact]
        public async Task IsEmployeeInGroupShouldReturnTrueIfEmployeeIsInGroup()
        {
            // Arrange
            this.FakeDb.Add(new EmployeeGroup() { UserId = UserId, GroupId = GroupId, Salary = Salary, Position = Position });

            this.employeeGroupService = new EmployeeGroupService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var isEmployeeInGroup = await this.employeeGroupService.IsEmployeeInGroup(UserId, GroupId);

            // Assert
            Assert.True(isEmployeeInGroup);
        }

        [Fact]
        public async Task IsEmployeeShouldReturnFalseIfEmplyoeeIsNotInTheGroup()
        {
            // Arrange
            this.employeeGroupService = new EmployeeGroupService(this.GetMockedRepositoryReturningAllAsNoTracking());

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
            var group1 = new Group() { Id = GroupId, BusinessId = BusinessId, Name = groupName1 };

            var employeeGroup = new EmployeeGroup
            {
                UserId = UserId,
                GroupId = GroupId,
                Salary = Salary,
                Position = Position,
                Group = group1,
            };

            this.FakeDb.Add(employeeGroup);
            this.employeeGroupService = new EmployeeGroupService(this.GetMockedRepositoryReturningAllAsNoTracking());

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

            this.FakeDb.Add(employeeGroup);
            this.employeeGroupService = new EmployeeGroupService(this.GetMockedRepositoryReturningAllAsNoTracking());

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

            this.FakeDb.Add(new EmployeeGroup() { Id = employeeId, UserId = UserId, GroupId = GroupId, Salary = Salary, Position = Position });
            this.employeeGroupService = new EmployeeGroupService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var getEmployeeId = await this.employeeGroupService.GetEmployeeId(UserId, GroupId);

            // Assert
            Assert.Equal(employeeId, getEmployeeId);
        }

        [Fact]
        public async Task GetEmployeeIdShouldReturnNullIfSuchDoNotExist()
        {
            // Arrange
            this.employeeGroupService = new EmployeeGroupService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var getEmployeeId = await this.employeeGroupService.GetEmployeeId(UserId, GroupId);

            // Assert
            Assert.Null(getEmployeeId);
        }
    }
}
