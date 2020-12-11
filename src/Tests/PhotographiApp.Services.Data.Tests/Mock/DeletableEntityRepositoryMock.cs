namespace PhotographiApp.Services.Data.Tests.Mock
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using PhotographiApp.Data.Common.Models;
    using PhotographiApp.Data.Common.Repositories;

    public class DeletableEntityRepositoryMock
    {
        public static Mock<IDeletableEntityRepository<TEntity>> Get<TEntity>(List<TEntity> dataset)
            where TEntity : class, IDeletableEntity
        {
            var repo = new Mock<IDeletableEntityRepository<TEntity>>();
            repo.Setup(x => x.All()).Returns(dataset.AsQueryable());
            repo.Setup(x => x.AllAsNoTracking()).Returns(dataset.AsQueryable());
            repo.Setup(x => x.AllAsNoTrackingWithDeleted()).Returns(dataset.AsQueryable());
            repo.Setup(x => x.AllWithDeleted()).Returns(dataset.AsQueryable());
            repo.Setup(x => x.AddAsync(It.IsAny<TEntity>())).Callback(
                (TEntity entity) => dataset.Add(entity));
            repo.Setup(x => x.Delete(It.IsAny<TEntity>())).Callback(
                (TEntity entity) => dataset.Remove(entity));

            return repo;
        }
    }
}
