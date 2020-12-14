namespace PlanShift.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using MockQueryable.Moq;
    using Moq;
    using PlanShift.Data.Common.Models;
    using PlanShift.Data.Common.Repositories;

    public abstract class DeletableEntityBaseTestClass : BaseTestClass
    {
        protected void SetMockedRepositoryCreateOperations<T>(Mock<IDeletableEntityRepository<T>> repository, List<T> fakeDb)
            where T : class, IDeletableEntity
        {
            repository.Setup(r => r.AddAsync(It.IsAny<T>()))
                .Callback(delegate (T entity)
                {
                    fakeDb.Add(entity);
                });

            repository.Setup(r => r.SaveChangesAsync());
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