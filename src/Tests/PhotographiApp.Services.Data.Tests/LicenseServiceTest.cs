namespace PhotographiApp.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Reflection;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Tests.Mock;
    using PhotographiApp.Services.Data.Tests.Seed;
    using PhotographiApp.Services.Mapping;
    using PhotographiApp.Web.ViewModels;
    using Xunit;

    public class LicenseServiceTest
    {
        public LicenseServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public void GetAll_ShouldReturnAll()
        {
            var licenses = new List<License>()
            {
                LicenseCreator.Create(),
                LicenseCreator.Create(),
                LicenseCreator.Create(),
            };

            var repo = DeletableEntityRepositoryMock.Get<License>(licenses);
            var service = new LicenseService(repo.Object);
            var result = service.GetAll();

            Assert.Equal(3, result.Count);
        }
    }
}
