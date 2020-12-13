namespace PlanShift.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using MockQueryable.Moq;
    using Moq;
    using PlanShift.Data.Common.Models;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Services.Mapping;
    using PlanShift.Web.ViewModels;

    public class BaseTestClass
    {
        public BaseTestClass()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        protected void SetMockedRepositoryCreateOperations<T>(Mock<IDeletableEntityRepository<T>> mockedRepository, List<T> fakeDb)
            where T : class, IDeletableEntity
        {
            mockedRepository.Setup(r => r.AddAsync(It.IsAny<T>()))
                .Callback(delegate (T entity)
                {
                    fakeDb.Add(entity);
                });

            mockedRepository.Setup(r => r.SaveChangesAsync());
        }

        protected void SetMockedRepositoryReturningAllAsNoTracking<T>(Mock<IDeletableEntityRepository<T>> mockedRepository, List<T> fakeDb)
            where T : class, IDeletableEntity
        {
            var mockQueryable = fakeDb.AsQueryable().BuildMock();
            mockedRepository.Setup(r => r.AllAsNoTracking())
                .Returns(mockQueryable.Object);
        }
    }
}