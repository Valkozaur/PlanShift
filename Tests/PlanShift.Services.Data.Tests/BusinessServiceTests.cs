namespace PlanShift.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using MockQueryable.Moq;
    using Moq;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.BusinessServices;
    using PlanShift.Services.Mapping;
    using PlanShift.Web.ViewModels;
    using PlanShift.Web.ViewModels.Business;
    using Xunit;

    public class BusinessServiceTests : DeletableEntityBaseTestClass
    {
        private const string TestBusinessName = "Test";
        private const string TestUserId = "Test";
        private const int TestTypeId = 0;


        private readonly Mock<IDeletableEntityRepository<Business>> repository;
        private readonly List<Business> fakeDb;
        private IBusinessService businessService;

        public BusinessServiceTests()
        {
            this.fakeDb = new List<Business>();
            this.repository = new Mock<IDeletableEntityRepository<Business>>();
        }

        [Fact]
        public async Task CreateBusinessAsyncShouldCreateNewBusinessInTheRepository()
        {
            // Arrange
            this.SetMockedRepositoryCreateOperations(this.repository, this.fakeDb);

            this.businessService = new BusinessService(this.repository.Object);

            // Act
            var id = await businessService.CreateBusinessAsync(TestUserId, TestBusinessName, TestTypeId);

            // Assert
            Assert.NotNull(id);
            Assert.Contains(this.fakeDb, x => x.Name == TestBusinessName && x.OwnerId == TestUserId && x.BusinessTypeId == TestTypeId);
        }

        [Fact]
        public async Task GetAllForUserAsyncShouldReturnProperElement()
        {
            // Arrange
            this.fakeDb.Add(new Business() { Name = TestBusinessName, OwnerId = TestUserId, BusinessTypeId = TestTypeId });
            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);

            this.businessService = new BusinessService(this.repository.Object);

            // Act
            var businessesForUser = await businessService.GetAllForUserAsync<BusinessInfoViewModel>(TestUserId);

            // Assert
            Assert.Single(businessesForUser);
            Assert.Contains(businessesForUser, b => b.Name == TestBusinessName);
        }

        [Fact]
        public async Task GetAllBusinessUserAsyncShouldReturnCountElementsWhenUserIsOwner()
        {
            const int countOfBusinesses = 2;

            // Arrange
            this.fakeDb.Add(new Business() { Name = TestBusinessName, OwnerId = TestUserId, BusinessTypeId = TestTypeId });
            this.fakeDb.Add(new Business() { Name = TestBusinessName + 1, OwnerId = TestUserId, BusinessTypeId = TestTypeId + 1 });
            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);

            this.businessService = new BusinessService(this.repository.Object);

            // Act
            var businessesForUser = await businessService.GetAllForUserAsync<BusinessInfoViewModel>(TestUserId, 2);

            // Assert
            Assert.Equal(countOfBusinesses, businessesForUser.Count());
            Assert.Contains(businessesForUser, b => b.Name == TestBusinessName);
        }

        [Fact]
        public async Task GetAllBusinessesShouldNotReturnIfUserDoesntHaveAnyBusinesses()
        {
            const int countOfBusinesses = 0;

            // Arrange
            var fakeUserId = Guid.NewGuid().ToString();

            this.fakeDb.Add(new Business() { Name = TestBusinessName, OwnerId = TestUserId, BusinessTypeId = TestTypeId });
            this.fakeDb.Add(new Business() { Name = TestBusinessName + 1, OwnerId = TestUserId, BusinessTypeId = TestTypeId + 1 });
            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);

            this.businessService = new BusinessService(this.repository.Object);

            // Act
            var businessesForUser = await businessService.GetAllForUserAsync<BusinessInfoViewModel>(fakeUserId, 2);

            // Assert
            Assert.Empty(businessesForUser);
        }

        // TODO: Test if user is employee in Business
        [Fact]
        public async Task GetBusinessAsyncShouldReturnBusinessWhenGivenExistingId()
        {
            const string testBusinessId = "Test";

            // Arrange
            this.fakeDb.Add(new Business() { Id = testBusinessId,  Name = TestBusinessName, OwnerId = TestUserId, BusinessTypeId = TestTypeId });
            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);


            this.businessService = new BusinessService(this.repository.Object);

            // Act
            var businesses = await businessService.GetBusinessAsync<BusinessInfoViewModel>(testBusinessId);

            // Assert
            Assert.Equal(testBusinessId, businesses.Id);
            Assert.Equal(TestBusinessName, businesses.Name);
        }

        [Fact]
        public async Task GetBusinessAsyncShouldNotReturnBusinessWhenGivenNonExistingBusinessId()
        {
            const string nonExistingBusinessId = "Test";

            // Arrange

            this.fakeDb.Add(new Business() { Name = TestBusinessName, OwnerId = TestUserId, BusinessTypeId = TestTypeId });
            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);

            this.businessService = new BusinessService(this.repository.Object);

            // Act
            var businesses = await businessService.GetBusinessAsync<BusinessInfoViewModel>(nonExistingBusinessId);

            // Assert
            Assert.Null(businesses);
        }

        [Fact]
        public async Task GetOwnerIdAsyncShouldReturnOwnerIdWhenGivenExistingBusinessId()
        {
            const string testBusinessId = "Test";

            // Arrange
            this.fakeDb.Add(new Business() { Id = testBusinessId,  Name = TestBusinessName, OwnerId = TestUserId, BusinessTypeId = TestTypeId });
            this.SetMockedRepositoryReturningAllAsNoTracking(this.repository, this.fakeDb);

            this.businessService = new BusinessService(this.repository.Object);

            // Act
            var ownerId = await businessService.GetOwnerIdAsync(testBusinessId);

            // Assert
            Assert.Equal(TestUserId, ownerId);
        }
    }
}
