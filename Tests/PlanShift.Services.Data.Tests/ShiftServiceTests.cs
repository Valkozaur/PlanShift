namespace PlanShift.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using PlanShift.Data.Models;
    using PlanShift.Data.Models.Enumerations;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Services.Data.Tests.BaseTestClasses;
    using PlanShift.Services.Mapping;
    using PlanShift.Web.ViewModels;
    using PlanShift.Web.ViewModels.Shift;
    using Xunit;

    public class ShiftServiceTests : DeletableEntityTestClass<Shift>
    {
        private const string ShiftCreatorId = "Test";
        private const string GroupId = "Test";
        private const string Description = "Test";
        private const decimal BonusPayment = 100M;

        private readonly DateTime start;
        private readonly DateTime end;
        private IShiftService shiftService;

        public ShiftServiceTests()
        {
            this.start = DateTime.UtcNow.AddDays(1);
            this.end = this.start.AddHours(8);
        }

        [Fact]
        public async Task CreateShiftShouldCreateAndReturnStringIdIfEverythingIsCorrect()
        {
            // Arrange
            this.shiftService = new ShiftService(this.GetMockedRepositoryWithCreateOperations());

            // Act
            var shiftId = await this.shiftService.CreateShiftAsync(ShiftCreatorId, GroupId, this.start, this.end, Description, BonusPayment);

            // Assert
            Assert.NotNull(shiftId);
            Assert.Single(this.FakeDb);
        }

        [Fact]
        public async Task ApproveShiftToEmployeeShouldChangeShiftStatusToApproved()
        {
            const string shiftId = "Test";

            // Arrange
            var shift = new Shift()
            {
                Id = shiftId,
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
            };

            this.FakeDb.Add(shift);

            this.shiftService = new ShiftService(this.GetMockedRepositoryAll());

            // Act
            await this.shiftService.ApproveShiftToEmployeeAsync(shiftId, ShiftCreatorId, ShiftCreatorId);

            // Assert
            Assert.Equal(ShiftStatus.Approved, shift.ShiftStatus);
            Assert.Equal(ShiftCreatorId, shift.EmployeeId);
        }

        [Fact]
        public void ApproveShiftToEmployeeShouldThrowIfNoShiftWasFound()
        {
            const string shiftId = "Test";
            const string fakeShiftId = "Fake";

            // Arrange
            var shift = new Shift()
            {
                Id = shiftId,
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
            };

            this.FakeDb.Add(shift);

            this.shiftService = new ShiftService(this.GetMockedRepositoryAll());

            // Act
            // Assert
            Assert.ThrowsAsync<ArgumentException>(() => this.shiftService.ApproveShiftToEmployeeAsync(fakeShiftId, ShiftCreatorId, ShiftCreatorId));
        }

        [Fact]
        public async Task GetShiftByIdShouldReturnShiftIfExisting()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            const string shiftId = "Test";

            // Arrange
            var shift = new Shift()
            {
                Id = shiftId,
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
            };

            this.FakeDb.Add(shift);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var shiftResult = await this.shiftService.GetShiftByIdAsync<ShiftTestViewModel>(shiftId);

            // Assert
            Assert.NotNull(shiftResult);
            Assert.Equal(shiftId, shiftResult.Id);
        }

        [Fact]
        public async Task GetShiftByIdShouldReturnNothingIfShiftDoesNotExist()
        {
            const string shiftId = "Test";
            const string fakeShiftId = "Fake";

            // Arrange
            var shift = new Shift()
            {
                Id = shiftId,
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
            };

            this.FakeDb.Add(shift);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var shiftResult = await this.shiftService.GetShiftByIdAsync<ShiftTestViewModel>(fakeShiftId);

            // Assert
            Assert.Null(shiftResult);
        }

        [Fact]
        public async Task GetAllShiftsByGroupShouldReturnAllShiftPerGivenGroup()
        {
            const int expectedShiftsCount = 2;

            // Arrange
            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
            };

            var shift2 = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
            };

            this.FakeDb.Add(shift);
            this.FakeDb.Add(shift2);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var shifts = await this.shiftService.GetAllShiftsByGroupAsync<ShiftTestViewModel>(GroupId);

            // Assert
            Assert.Equal(expectedShiftsCount, shifts.Count);
            Assert.All(shifts, model => Equals(model.GroupId, GroupId));
        }

        [Fact]
        public async Task GetAllShiftsByGroupShouldNotReturnAnythingIfNoShiftsPerGroup()
        {
            const string fakeGroupId = "Fake";

            // Arrange
            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
            };

            var shift2 = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
            };

            this.FakeDb.Add(shift);
            this.FakeDb.Add(shift2);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var shifts = await this.shiftService.GetAllShiftsByGroupAsync<ShiftTestViewModel>(fakeGroupId);

            // Assert
            Assert.Empty(shifts);
        }

        [Fact]
        public async Task GetPendingShiftsPerGroupShouldReturnAllShiftsWithPendingStatusPerGroup()
        {
            const int expectedResult = 2;

            // Arrange
            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Pending,
            };

            var shift2 = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Pending,
            };

            this.FakeDb.Add(shift);
            this.FakeDb.Add(shift2);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var shiftsPerGroup = await this.shiftService.GetPendingShiftsPerGroupAsync<ShiftTestViewModel>(GroupId);

            // Assert
            Assert.Equal(expectedResult, shiftsPerGroup.Count());
            Assert.All(shiftsPerGroup, model => Equals(model.GroupId, GroupId));
            Assert.All(shiftsPerGroup, model => Equals(model.ShiftStatus, ShiftStatus.Pending));
        }

        [Fact]
        public async Task GetPendingShiftsPerGroupShouldReturnNothingIfNoPendingShiftsInGroup()
        {
            const string fakeGroupId = "Test";

            // Arrange
            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
            };

            var shift2 = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
            };

            this.FakeDb.Add(shift);
            this.FakeDb.Add(shift2);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var shifts = await this.shiftService.GetPendingShiftsPerGroupAsync<ShiftTestViewModel>(fakeGroupId);

            // Assert
            Assert.Empty(shifts);
        }

        [Fact]
        public async Task GetPendingShiftsPerGroupShouldReturnNothingIfNoShiftsInGroup()
        {
            const string fakeGroupId = "Fake";

            // Arrange
            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Pending,
            };

            var shift2 = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Pending,
            };

            this.FakeDb.Add(shift);
            this.FakeDb.Add(shift2);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var shifts = await this.shiftService.GetPendingShiftsPerGroupAsync<ShiftTestViewModel>(fakeGroupId);

            // Assert
            Assert.Empty(shifts);
        }

        [Fact]
        public async Task GetUpcomingShiftForUserShouldReturnAllApprovedShiftsPerUser()
        {
            const int expectedResult = 2;
            const string businessId = "Test";
            const string userId = "Test";

            // Arrange
            var group = new Group { BusinessId = businessId };
            var employee = new EmployeeGroup() { UserId = userId };

            var shift = new Shift()
            {
                Start = this.start,
                End = DateTime.UtcNow.AddDays(1),
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Approved,
                Group = group,
                Employee = employee,
            };

            var shift2 = new Shift()
            {
                Start = this.start,
                End = DateTime.UtcNow.AddDays(1),
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Approved,
                Group = group,
                Employee = employee,
            };

            this.FakeDb.Add(shift);
            this.FakeDb.Add(shift2);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var employeeUpcomingShifts = await this.shiftService.GetUpcomingShiftForUserAsync<ShiftTestViewModel>(businessId, userId);

            // Assert
            Assert.Equal(expectedResult, employeeUpcomingShifts.Count());
            Assert.All(employeeUpcomingShifts, model => Equals(model.EmployeeId, userId));
            Assert.All(employeeUpcomingShifts, model => Equals(model.ShiftStatus, ShiftStatus.Approved));
        }

        [Fact]
        public async Task GetUpcomingShiftForUserShouldNotReturnShiftsIfNoApprovedOneExist()
        {
            const string businessId = "Test";
            const string userId = "Test";

            // Arrange
            var group = new Group { BusinessId = businessId };
            var employee = new EmployeeGroup() { UserId = userId };

            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
                Group = group,
                Employee = employee,
            };

            this.FakeDb.Add(shift);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var employeeUpcomingShifts = await this.shiftService.GetUpcomingShiftForUserAsync<ShiftTestViewModel>(businessId, userId);

            // Assert
            Assert.Empty(employeeUpcomingShifts);
        }

        [Fact]
        public async Task GetUpcomingShiftForUserShouldNotReturnShiftsIfEndedAlready()
        {
            const string businessId = "Test";
            const string userId = "Test";

            // Arrange
            var group = new Group { BusinessId = businessId };
            var employee = new EmployeeGroup() { UserId = userId };


            var shift = new Shift()
            {
                Start = this.start,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Pending,
                End = DateTime.UtcNow.AddDays(-1),
                Group = group,
                Employee = employee,
            };

            this.FakeDb.Add(shift);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var employeeUpcomingShifts = await this.shiftService.GetUpcomingShiftForUserAsync<ShiftTestViewModel>(businessId, userId);

            // Assert
            Assert.Empty(employeeUpcomingShifts);
        }

        [Fact]
        public async Task GetUpcomingShiftForUserShouldNoShiftsInTheBusiness()
        {
            const string businessId = "Test";
            const string fakeBusinessId = "Fake";
            const string userId = "Test";
            var employee = new EmployeeGroup() { UserId = userId };


            // Arrange
            var group = new Group { BusinessId = businessId };

            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Pending,
                Group = group,
                Employee = employee,
            };

            this.FakeDb.Add(shift);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var employeeUpcomingShifts = await this.shiftService.GetUpcomingShiftForUserAsync<ShiftTestViewModel>(fakeBusinessId, userId);

            // Assert
            Assert.Empty(employeeUpcomingShifts);
        }

        [Fact]
        public async Task GetUpcomingShiftForUserShouldNoShiftsForEmployee()
        {
            const string businessId = "Test";
            const string userId = "Test";
            const string fakeEmployeeId = "Fake";

            // Arrange
            var group = new Group { BusinessId = businessId };
            var employee = new EmployeeGroup() { UserId = userId };

            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Pending,
                Group = group,
                Employee = employee,
            };

            this.FakeDb.Add(shift);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var employeeUpcomingShifts = await this.shiftService.GetUpcomingShiftForUserAsync<ShiftTestViewModel>(businessId, fakeEmployeeId);

            // Assert
            Assert.Empty(employeeUpcomingShifts);
        }

        [Fact]
        public async Task GetOpenShiftsAvailableForUserShouldReturnAllShiftsOpenForUserToApply()
        {
            const int expectedCount = 2;
            const string businessId = "Test";
            const string userId = "Test";

            // Arrange
            var group = new Group { BusinessId = businessId };
            var employee = new EmployeeGroup() { UserId = userId };
            group.Employees.Add(employee);

            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
                Group = group,
            };

            var shift2 = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
                Group = group,
            };

            this.FakeDb.Add(shift);
            this.FakeDb.Add(shift2);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var availableShifts = await this.shiftService.GetOpenShiftsAvailableForUserAsync<ShiftTestViewModel>(businessId, userId);

            // Assert
            Assert.NotEmpty(availableShifts);
            Assert.Equal(expectedCount, availableShifts.Count());
        }

        [Fact]
        public async Task GetOpenShiftsAvailableForUserShouldReturnNoShiftIfShiftStatusAreNotOpen()
        {
            const string businessId = "Test";
            const string userId = "Test";

            // Arrange
            var group = new Group { BusinessId = businessId };
            var employee = new EmployeeGroup() { UserId = userId };
            group.Employees.Add(employee);

            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Pending,
                Group = group,
            };

            var shift2 = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Pending,
                Group = group,
            };

            this.FakeDb.Add(shift);
            this.FakeDb.Add(shift2);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var availableShifts = await this.shiftService.GetOpenShiftsAvailableForUserAsync<ShiftTestViewModel>(businessId, userId);

            // Assert
            Assert.Empty(availableShifts);
        }

        [Fact]
        public async Task GetOpenShiftsAvailableForUserShouldReturnNoShiftsIfEmployeeIsNotInGroup()
        {
            const string businessId = "Test";
            const string userId = "Test";

            // Arrange
            var group = new Group { BusinessId = businessId };

            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
                Group = group,
            };

            var shift2 = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
                Group = group,
            };

            this.FakeDb.Add(shift);
            this.FakeDb.Add(shift2);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var availableShifts = await this.shiftService.GetOpenShiftsAvailableForUserAsync<ShiftTestViewModel>(businessId, userId);

            // Assert
            Assert.Empty(availableShifts);
        }

        [Fact]
        public async Task GetUsersShiftsWithDeclaredSwapRequestsAsyncShouldReturnAllPendingShiftsIfEverythingIsCorrect()
        {
            const int expectedCount = 2;
            const string businessId = "Test";
            const string userId = "Test";

            // Arrange
            var group = new Group { BusinessId = businessId };
            var shiftChange = new ShiftChange { OriginalEmployee = new EmployeeGroup { UserId = userId } };

            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Pending,
                Group = group,
            };
            shift.ShiftChanges.Add(shiftChange);

            var shift2 = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Pending,
                Group = group,
            };
            shift2.ShiftChanges.Add(shiftChange);

            this.FakeDb.Add(shift);
            this.FakeDb.Add(shift2);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var pendingShifts = await this.shiftService.GetUsersShiftsWithDeclaredSwapRequestsAsync<ShiftTestViewModel>(businessId, userId);

            // Assert
            Assert.NotEmpty(pendingShifts);
            Assert.Equal(expectedCount, pendingShifts.Count());
        }

        [Fact]
        public async Task GetUsersShiftsWithDeclaredSwapRequestsAsyncShouldNotReturnAnythingIfNoShiftChangePerUser()
        {

            const string businessId = "Test";
            const string userId = "Test";
            const string fakeUserId = "Fake";

            // Arrange
            var group = new Group { BusinessId = businessId };
            var shiftChange = new ShiftChange { OriginalEmployee = new EmployeeGroup { UserId = fakeUserId } };

            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Pending,
                Group = group,
            };
            shift.ShiftChanges.Add(shiftChange);

            var shift2 = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Pending,
                Group = group,
            };
            shift2.ShiftChanges.Add(shiftChange);

            this.FakeDb.Add(shift);
            this.FakeDb.Add(shift2);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var pendingShifts = await this.shiftService.GetUsersShiftsWithDeclaredSwapRequestsAsync<ShiftTestViewModel>(businessId, userId);

            // Assert
            Assert.Empty(pendingShifts);
        }

        [Fact]
        public async Task GetTakenShiftsPerUserAsyncShouldReturnAllShiftsEmployeeNotTheUser()
        {
            const int expectedCount = 2;
            const string businessId = "Test";
            const string userId = "Test";

            // Arrange
            var group = new Group { BusinessId = businessId };
            var employeeGroup = new EmployeeGroup { UserId = "some other userId" };

            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Approved,
                Group = group,
                Employee = employeeGroup,
            };
            var shift2 = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Approved,
                Group = group,
                Employee = employeeGroup,
            };

            this.FakeDb.Add(shift);
            this.FakeDb.Add(shift2);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var pendingShifts = await this.shiftService.GetTakenShiftsPerUserAsync<ShiftTestViewModel>(businessId, userId);

            // Assert
            Assert.NotEmpty(pendingShifts);
            Assert.Equal(expectedCount, pendingShifts.Count());
        }

        [Fact]
        public async Task GetTakenShiftsPerUserAsyncShouldNotReturnIfUserIsEmployee()
        {
            const int expectedCount = 2;
            const string businessId = "Test";
            const string userId = "Test";

            // Arrange
            var group = new Group { BusinessId = businessId };
            var employeeGroup = new EmployeeGroup { UserId = userId };

            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Approved,
                Group = group,
                Employee = employeeGroup,
            };
            var shift2 = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Approved,
                Group = group,
                Employee = employeeGroup,
            };

            this.FakeDb.Add(shift);
            this.FakeDb.Add(shift2);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var pendingShifts = await this.shiftService.GetTakenShiftsPerUserAsync<ShiftTestViewModel>(businessId, userId);

            // Assert
            Assert.Empty(pendingShifts);
        }

        [Fact]
        public async Task GetTakenShiftsPerUserAsyncShouldNotReturnIfShiftsHasNoEmployee()
        {
            const int expectedCount = 2;
            const string businessId = "Test";
            const string userId = "Test";

            // Arrange
            var group = new Group { BusinessId = businessId };

            var shift = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Pending,
                Group = group,
            };
            var shift2 = new Shift()
            {
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Pending,
                Group = group,
            };

            this.FakeDb.Add(shift);
            this.FakeDb.Add(shift2);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var pendingShifts = await this.shiftService.GetTakenShiftsPerUserAsync<ShiftTestViewModel>(businessId, userId);

            // Assert
            Assert.Empty(pendingShifts);
        }
    }
}
