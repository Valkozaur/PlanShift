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

    public class BusinessServiceTests : BaseTestClass
    {
        private const string TestBusinessName = "Test";

        private readonly string testUserId;
        private readonly int testTypeId;

        private readonly Mock<IDeletableEntityRepository<Business>> repository;
        private readonly List<Business> fakeDb;

        public BusinessServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.testUserId = Guid.NewGuid().ToString();
            this.testTypeId = 0;

            this.fakeDb = new List<Business>();
            this.repository = new Mock<IDeletableEntityRepository<Business>>();
        }

        [Fact]
        public async Task CreateBusinessAsyncShouldCreateNewBusinsessInTheRepository()
        {
            // Arrange
            this.SetMockedRepositoryCreateOperations(this.repository, this.fakeDb);

            var businessService = new BusinessService(this.repository.Object);

            // Act
            var id = await businessService.CreateBusinessAsync(this.testUserId, TestBusinessName, this.testTypeId);

            // Assert
            Assert.NotNull(id);
            Assert.Contains(this.fakeDb, x => x.Name == TestBusinessName && x.OwnerId == this.testUserId && x.BusinessTypeId == this.testTypeId);
        }

        [Fact]
        public async Task GetAllForUserAsyncShouldReturnProperElement()
        {
            // Arrange
            this.fakeDb.Add(new Business() { Name = TestBusinessName, OwnerId = this.testUserId, BusinessTypeId = this.testTypeId });
            var mockQueryable = this.fakeDb.AsQueryable().BuildMock();
            this.repository.Setup(r => r.AllAsNoTracking())
                .Returns(mockQueryable.Object);

            var businessService = new BusinessService(this.repository.Object);

            // Act
            var businessesForUser = await businessService.GetAllForUserAsync<BusinessInfoViewModel>(this.testUserId);

            // Assert
            Assert.Single(businessesForUser);
            Assert.Contains(businessesForUser, b => b.Name == TestBusinessName);
        }

        [Fact]
        public async Task GetAllBusinessUserAsyncShouldReturnCountElementsWhenUserIsOwner()
        {
            const int countOfBusinesses = 2;

            // Arrange
            this.fakeDb.Add(new Business() { Name = TestBusinessName, OwnerId = this.testUserId, BusinessTypeId = this.testTypeId });
            this.fakeDb.Add(new Business() { Name = TestBusinessName + 1, OwnerId = this.testUserId, BusinessTypeId = this.testTypeId + 1 });
            var mockQueryable = this.fakeDb.AsQueryable().BuildMock();
            this.repository.Setup(r => r.AllAsNoTracking())
                .Returns(mockQueryable.Object);

            var businessService = new BusinessService(this.repository.Object);

            // Act
            var businessesForUser = await businessService.GetAllForUserAsync<BusinessInfoViewModel>(this.testUserId, 2);

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

            this.fakeDb.Add(new Business() { Name = TestBusinessName, OwnerId = this.testUserId, BusinessTypeId = this.testTypeId });
            this.fakeDb.Add(new Business() { Name = TestBusinessName + 1, OwnerId = this.testUserId, BusinessTypeId = this.testTypeId + 1 });
            var mockQueryable = this.fakeDb.AsQueryable().BuildMock();

            this.repository.Setup(r => r.AllAsNoTracking())
                .Returns(mockQueryable.Object);

            var businessService = new BusinessService(this.repository.Object);

            // Act
            var businessesForUser = await businessService.GetAllForUserAsync<BusinessInfoViewModel>(fakeUserId, 2);

            // Assert
            Assert.Equal(countOfBusinesses, businessesForUser.Count());
        }

        // TODO: Test if user is employee in Business
        [Fact]
        public async Task GetBusinessAsyncShouldReturnBusinessWhenGivenExistingId()
        {
            // Arrange
            this.fakeDb.Add(new Business() { Name = TestBusinessName, OwnerId = this.testUserId, BusinessTypeId = this.testTypeId });
            var businessId = this.fakeDb.FirstOrDefault()?.Id;
            var mockQueryable = this.fakeDb.AsQueryable().BuildMock();

            this.repository.Setup(r => r.AllAsNoTracking())
                .Returns(mockQueryable.Object);

            var businessService = new BusinessService(this.repository.Object);

            // Act
            var businesses = await businessService.GetBusinessAsync<BusinessInfoViewModel>(businessId);

            // Assert
            Assert.Equal(businessId, businesses.Id);
            Assert.Equal(TestBusinessName, businesses.Name);
        }

        [Fact]
        public async Task GetBusinessAsyncShouldNotReturnBusinessWhenGivenNonExistingBusinessId()
        {
            // Arrange
            var nonExistingBusinessId = Guid.NewGuid().ToString();

            this.fakeDb.Add(new Business() { Name = TestBusinessName, OwnerId = this.testUserId, BusinessTypeId = this.testTypeId });
            var mockQueryable = this.fakeDb.AsQueryable().BuildMock();

            this.repository.Setup(r => r.AllAsNoTracking())
                .Returns(mockQueryable.Object);

            var businessService = new BusinessService(this.repository.Object);

            // Act
            var businesses = await businessService.GetBusinessAsync<BusinessInfoViewModel>(nonExistingBusinessId);

            // Assert
            Assert.Null(businesses);
        }

        [Fact]
        public async Task GetOwnerIdAsyncShouldReturnOwnerIdWhenGivenExistingBusinessId()
        {
            // Arrange
            this.fakeDb.Add(new Business() { Name = TestBusinessName, OwnerId = this.testUserId, BusinessTypeId = this.testTypeId });
            var mockQueryable = this.fakeDb.AsQueryable().BuildMock();

            this.repository.Setup(r => r.AllAsNoTracking())
                .Returns(mockQueryable.Object);

            var businessService = new BusinessService(this.repository.Object);
            var businessId = this.fakeDb.FirstOrDefault()?.Id;

            // Act
            var ownerId = await businessService.GetOwnerIdAsync(businessId);

            // Assert
            Assert.Equal(this.testUserId, ownerId);
        }
    }
}
