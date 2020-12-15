namespace PlanShift.Services.Data.Tests.BaseTestClasses
{
    using System.Collections.Generic;
    using System.Linq;

    using MockQueryable.Moq;
    using Moq;
    using PlanShift.Data.Common.Models;
    using PlanShift.Data.Common.Repositories;

    public abstract class DeletableEntityBaseTestClass<T>
        where T : class, IDeletableEntity
    {
        protected DeletableEntityBaseTestClass()
        {
            this.Repository = new Mock<IDeletableEntityRepository<T>>();
            this.FakeDb = new List<T>();
        }

        protected Mock<IDeletableEntityRepository<T>> Repository { get; set; }

        protected List<T> FakeDb { get; set; }

        protected IDeletableEntityRepository<T> GetMockedRepositoryWithCreateOperations()
        {
            this.Repository.Setup(r => r.AddAsync(It.IsAny<T>()))
                .Callback(delegate (T entity)
                {
                    this.FakeDb.Add(entity);
                });

            this.Repository.Setup(r => r.SaveChangesAsync());

            return this.Repository.Object;
        }

        protected IDeletableEntityRepository<T> GetMockedRepositoryReturningAllAsNoTracking()
        {
            var mockQueryable = this.FakeDb.AsQueryable().BuildMock();
            this.Repository.Setup(r => r.AllAsNoTracking())
                .Returns(mockQueryable.Object);

            return this.Repository.Object;
        }

        protected IDeletableEntityRepository<T> GetMockedRepositoryAll()
        {
            var queryableMock = this.FakeDb.AsQueryable().BuildMock();
            this.Repository.Setup(r => r.All())
                .Returns(queryableMock.Object);

            return this.Repository.Object;
        }
    }
}