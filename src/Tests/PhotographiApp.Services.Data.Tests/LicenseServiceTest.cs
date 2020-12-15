namespace PhotographiApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Tests.Mock;
    using PhotographiApp.Services.Data.Tests.Seed;
    using PhotographiApp.Services.Mapping;
    using PhotographiApp.Web.ViewModels;
    using PhotographiApp.Web.ViewModels.Administration.Licenses;
    using Xunit;

    public class LicenseServiceTest
    {
        public LicenseServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public void NonGenericGetAll_ShouldReturnAll()
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

        [Fact]
        public void GenericGetAll_ShouldReturnAll()
        {
            var licenses = new List<License>()
            {
                LicenseCreator.Create(),
                LicenseCreator.Create(),
                LicenseCreator.Create(),
            };

            var repo = DeletableEntityRepositoryMock.Get<License>(licenses);
            var service = new LicenseService(repo.Object);
            var result = service.GetAll<LicenseViewModel>();

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void Create_ShouldExecuteSuccessfully()
        {
            var model = new CreateLicenseViewModel()
            {
                Name = "test",
                Description = "test",
            };

            var list = new List<License>();

            var repo = DeletableEntityRepositoryMock.Get<License>(list);
            var service = new LicenseService(repo.Object);
            service.CreateAsync(model).Wait();

            Assert.Single(list);
        }

        [Fact]
        public void Update_ShouldThrowExceptionWhenIdNotFound()
        {
            var model = new EditLicenseViewModel() { Id = "id", Name = "name", Description = "desc" };

            var repo = DeletableEntityRepositoryMock.Get<License>(new List<License>());
            var service = new LicenseService(repo.Object);
            var exception = Assert.Throws<AggregateException>(() => service.UpdateAsync(model).Wait());

            Assert.Contains("License does not exist!", exception.Message);
        }

        [Fact]
        public void Update_ShouldRunSuccessfully()
        {
            var license = LicenseCreator.Create();
            var list = new List<License>() { license };
            var model = new EditLicenseViewModel() { Id = license.Id, Name = "Updated", Description = "Updated" };

            var repo = DeletableEntityRepositoryMock.Get<License>(list);
            var service = new LicenseService(repo.Object);
            service.UpdateAsync(model).Wait();

            Assert.Single(list);
            Assert.Equal("Updated", list.First().Name);
            Assert.Equal("Updated", list.First().Description);
        }

        [Fact]
        public void Delete_ShouldThrowExceptionWhenIdNotFound()
        {
            var repo = DeletableEntityRepositoryMock.Get<License>(new List<License>());
            var service = new LicenseService(repo.Object);
            var exception = Assert.Throws<AggregateException>(() => service.DeleteAsync("id").Wait());

            Assert.Contains("License does not exist!", exception.Message);
        }

        [Fact]
        public void Delete_ShouldExecuteSuccessfully()
        {
            var license = LicenseCreator.Create();
            var list = new List<License>() { license };
            var repo = DeletableEntityRepositoryMock.Get<License>(list);
            var service = new LicenseService(repo.Object);
            service.DeleteAsync(license.Id).Wait();

            Assert.Empty(list);
        }

        [Fact]
        public void GetById_ShouldReturnValue()
        {
            var license = LicenseCreator.Create();
            var list = new List<License>() { license };
            var repo = DeletableEntityRepositoryMock.Get<License>(list);
            var service = new LicenseService(repo.Object);
            var result = service.GetById<LicenseViewModel>(license.Id);

            Assert.NotNull(license);
            Assert.Equal(license.Id, result.Id);
        }
    }
}
