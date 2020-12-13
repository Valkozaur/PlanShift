using System.Reflection;
using PlanShift.Services.Mapping;
using PlanShift.Web.ViewModels;

namespace PlanShift.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MockQueryable.Moq;
    using Moq;

    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.BusinessTypeServices;
    using PlanShift.Web.ViewModels.Business;

    using Xunit;

    public class BusinessTypeServiceTests : BaseTestClass
    {
        private const string TestBusinessTypeName = "Test";

        private readonly Mock<IRepository<BusinessType>> repository;
        private readonly IList<BusinessType> fakeDb;

        public BusinessTypeServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.fakeDb = new List<BusinessType>();
            this.repository = new Mock<IRepository<BusinessType>>();
        }

        [Fact]
        public async Task CreateBusinessTypeShouldWorkCorrectly()
        {
            // Arrange
            this.repository.Setup(r => r.AddAsync(It.IsAny<BusinessType>()))
                .Callback(delegate (BusinessType businessType)
                {
                    this.fakeDb.Add(businessType);
                });
            this.repository.Setup(r => r.SaveChangesAsync());

            var businessTypeService = new BusinessTypeService(this.repository.Object);

            // Act
            var id = await businessTypeService.CreateAsync(TestBusinessTypeName);

            // Assert
            Assert.Single(this.fakeDb);
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnAllExistingTypes()
        {
            // Arrange
            const int TestBusinessTypesCount = 3;

            this.fakeDb.Add(new BusinessType() { Id = 0, Name = TestBusinessTypeName });
            this.fakeDb.Add(new BusinessType() { Id = 1, Name = TestBusinessTypeName + 1 });
            this.fakeDb.Add(new BusinessType() { Id = 2, Name = TestBusinessTypeName + 2 });
            var mockQueryable = this.fakeDb.AsQueryable().BuildMock();

            this.repository.Setup(r => r.AllAsNoTracking())
                .Returns(mockQueryable.Object);

            var businessService = new BusinessTypeService(this.repository.Object);

            // Act
            var businessTypes = await businessService.GetAllAsync<BusinessTypeDropDownViewModel>();

            // Assert
            Assert.Equal(TestBusinessTypesCount, businessTypes.Count());
        }
    }
}
