namespace PlanShift.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;

    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Data.Models.Enumerations;
    using PlanShift.Services.Data.BusinessServices;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.Enumerations;
    using PlanShift.Services.Data.GroupServices;
    using PlanShift.Services.Data.Tests.BaseTestClasses;
    using PlanShift.Web.ViewModels.Group;

    using Xunit;

    public class GroupServiceTests : DeletableEntityTestClass<Group>
    {
        private const string Id = "Test";
        private const string Name = "Test";
        private const string BusinessId = "Test";
        private const decimal StandardSalary = 1000.10M;

        private GroupService groupService;

        [Fact]
        public async Task CreateGroupShouldWorkCorrectlyWhenGivenProperInformation()
        {
            // Arrange
            var employeeGroupService = Mock.Of<IEmployeeGroupService>(eg
                => eg.AddEmployeeToGroupAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<decimal>(),
                    It.IsAny<string>()) == Task.FromResult<string>(null));
            var businessService = Mock.Of<IBusinessService>(bs => bs.GetOwnerIdAsync(It.IsAny<string>()) == Task.FromResult<string>(null));

            this.groupService = new GroupService(this.GetMockedRepositoryWithCreateOperations(), employeeGroupService, businessService);

            // Act
            var id = await this.groupService.CreateGroupAsync(BusinessId, Name, StandardSalary);

            // Assert
            Assert.NotNull(id);
        }

        [Fact]
        public async Task GetGroupAsyncShouldReturnGroupWhenGivenRightId()
        {
            // Arrange
            var employeeGroupService = new Mock<IEmployeeGroupService>();
            var businessService = new Mock<IBusinessService>();

            this.FakeDb.Add(new Group() { Id = Id, BusinessId = BusinessId, Name = Name, StandardSalary = StandardSalary });
            ;

            this.groupService = new GroupService(this.GetMockedRepositoryReturningAllAsNoTracking(), employeeGroupService.Object, businessService.Object);

            // Act
            var group = await this.groupService.GetGroupAsync<GroupBasicInfoViewModel>(Id);

            // Assert
            Assert.NotNull(group);
        }

        [Fact]
        public async Task GetAllGroupByCurrentUserAndBusinessIdAsyncShouldReturnGroups()
        {
            const int groupsCount = 2;
            const string userId = "Test";

            // Arrange
            var employeeGroupService = new Mock<IEmployeeGroupService>();
            var businessService = new Mock<IBusinessService>();

            var group1 = new Group() { Id = Id, BusinessId = BusinessId, Name = Name, StandardSalary = StandardSalary };
            group1.Employees.Add(new EmployeeGroup() { UserId = userId });

            var group2 = new Group() { Id = Id + 1, BusinessId = BusinessId, Name = Name + 1, StandardSalary = StandardSalary };
            group2.Employees.Add(new EmployeeGroup() { UserId = userId });

            var group3 = new Group() { Id = Id + 11, BusinessId = BusinessId + 1, Name = Name + 11, StandardSalary = StandardSalary };
            group3.Employees.Add(new EmployeeGroup() { UserId = userId });

            this.FakeDb.Add(group1);
            this.FakeDb.Add(group2);
            this.FakeDb.Add(group3);

            this.groupService = new GroupService(this.GetMockedRepositoryReturningAllAsNoTracking(), employeeGroupService.Object, businessService.Object);

            // Act
            var groups = await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupBasicInfoViewModel>(BusinessId, userId);

            // Assert
            Assert.Equal(groupsCount, groups.Count());
            Assert.Contains(groups, g => g.Name == Name);
        }

        [Fact]
        public async Task GetAllGroupByCurrentUserAndBusinessIdAsyncShouldReturnGroupsWhichContainsShiftsWithShiftApplicationsWithPendingStatus()
        {
            const string userId = "Test";

            // Arrange
            var employeeGroupService = new Mock<IEmployeeGroupService>();
            var businessService = new Mock<IBusinessService>();

            var group1 = new Group() { Id = Id, BusinessId = BusinessId, Name = Name, StandardSalary = StandardSalary };
            group1.Employees.Add(new EmployeeGroup() { UserId = userId });

            var shift1 = new Shift();
            shift1.ShiftApplications.Add(new ShiftApplication() { Status = ShiftApplicationStatus.Pending });
            group1.Shifts.Add(shift1);

            var group2 = new Group() { Id = Id + 1, BusinessId = BusinessId, Name = Name + 1, StandardSalary = StandardSalary };
            group2.Employees.Add(new EmployeeGroup() { UserId = userId });

            var group3 = new Group() { Id = Id + 11, BusinessId = BusinessId + 1, Name = Name + 11, StandardSalary = StandardSalary };
            group3.Employees.Add(new EmployeeGroup() { UserId = userId });

            this.FakeDb.Add(group1);
            this.FakeDb.Add(group2);
            this.FakeDb.Add(group3);
         
            this.groupService = new GroupService(this.GetMockedRepositoryReturningAllAsNoTracking(), employeeGroupService.Object, businessService.Object);

            // Act
            var groups =
                await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupBasicInfoViewModel>(BusinessId, userId, PendingActionsType.ShiftApplications);

            // Assert
            Assert.Single(groups);
            Assert.Contains(groups, g => g.Name == Name);
        }

        [Fact]
        public async Task GetAllGroupByCurrentUserAndBusinessIdAsyncShouldReturnGroupsWhichContainsShiftsWithShiftChangesWithPendingStatus()
        {
            const string userId = "Test";

            // Arrange
            var employeeGroupService = new Mock<IEmployeeGroupService>();
            var businessService = new Mock<IBusinessService>();

            var group1 = new Group() { Id = Id, BusinessId = BusinessId, Name = Name, StandardSalary = StandardSalary };
            group1.Employees.Add(new EmployeeGroup() { UserId = userId });

            var shift1 = new Shift();
            shift1.ShiftChanges.Add(new ShiftChange() { Status = ShiftApplicationStatus.Pending });
            group1.Shifts.Add(shift1);

            var group2 = new Group() { Id = Id + 1, BusinessId = BusinessId, Name = Name + 1, StandardSalary = StandardSalary };
            group2.Employees.Add(new EmployeeGroup() { UserId = userId });

            var group3 = new Group() { Id = Id + 11, BusinessId = BusinessId + 1, Name = Name + 11, StandardSalary = StandardSalary };
            group3.Employees.Add(new EmployeeGroup() { UserId = userId });

            this.FakeDb.Add(group1);
            this.FakeDb.Add(group2);
            this.FakeDb.Add(group3);

            this.groupService = new GroupService(this.GetMockedRepositoryReturningAllAsNoTracking(), employeeGroupService.Object, businessService.Object);

            // Act
            var groups =
                await this.groupService.GetAllGroupByCurrentUserAndBusinessIdAsync<GroupBasicInfoViewModel>(BusinessId, userId, PendingActionsType.ShiftChanges);

            // Assert
            Assert.Single(groups);
            Assert.Contains(groups, g => g.Name == Name);
        }
    }
}
