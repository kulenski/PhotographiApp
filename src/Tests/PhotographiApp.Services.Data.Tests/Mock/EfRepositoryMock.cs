namespace PhotographiApp.Services.Data.Tests.Mock
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using PhotographiApp.Data.Common.Repositories;

    public class EfRepositoryMock
    {
        public static Mock<IRepository<TEntity>> Get<TEntity>(List<TEntity> dataset)
            where TEntity : class
        {
            var repo = new Mock<IRepository<TEntity>>();
            repo.Setup(x => x.All()).Returns(dataset.AsQueryable());
            repo.Setup(x => x.AllAsNoTracking()).Returns(dataset.AsQueryable());
            repo.Setup(x => x.AddAsync(It.IsAny<TEntity>())).Callback(
                (TEntity entity) => dataset.Add(entity));
            repo.Setup(x => x.Delete(It.IsAny<TEntity>())).Callback(
                (TEntity entity) => dataset.Remove(entity));

            return repo;
        }
    }
}
