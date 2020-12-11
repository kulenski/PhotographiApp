namespace PhotographiApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using Microsoft.AspNetCore.Http.Internal;
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Tests.Mock;
    using PhotographiApp.Services.Data.Tests.Seed;
    using PhotographiApp.Web.ViewModels.Photos;
    using Xunit;

    public class PhotoServiceTests
    {
        [Fact]
        public void Create_ShouldNotCreateWhenExtensionIsNotCorrect()
        {
            var user = UserCreator.Create("test");
            var file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
            var model = new CreatePhotoInputModel()
            {
                Title = "Test",
                Description = "Test",
                IsCommentAllowed = false,
                IsPrivate = false,
                LicenseId = "Test",
                File = file,
            };
            var list = new List<Photo>();

            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(list);
            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(new List<PhotoAlbum>());
            var storageService = PhotoStorageServiceMock.Get();

            var service = new PhotoService(photosRepo.Object, storageService.Object, photoAlbumsRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => service.CreatePhotoAsync(user.Id, "path", model).Wait());

            Assert.Contains("Invalid image extension", ex.Message);
            Assert.Empty(list);
        }

        [Fact]
        public void Create_ShouldCreateSuccessfully()
        {
            var user = UserCreator.Create("test");
            var file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.png");
            var model = new CreatePhotoInputModel()
            {
                Title = "Test",
                Description = "Test",
                IsCommentAllowed = false,
                IsPrivate = false,
                LicenseId = "Test",
                File = file,
            };
            var list = new List<Photo>();

            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(list);
            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(new List<PhotoAlbum>());
            var storageService = PhotoStorageServiceMock.Get();

            var service = new PhotoService(photosRepo.Object, storageService.Object, photoAlbumsRepo.Object);
            service.CreatePhotoAsync(user.Id, "path", model).Wait();

            Assert.Single(list);
        }

        [Fact]
        public void Update_ShouldThrowExceptionWhenUserIsNotOwner()
        {
            var user = UserCreator.Create("test");
            var visitor = UserCreator.Create("visitor");
            var photo = PhotoCreator.Create(user, false, false);

            var list = new List<Photo>() { photo };
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(list);
            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(new List<PhotoAlbum>());
            var storageService = PhotoStorageServiceMock.Get();

            var service = new PhotoService(photosRepo.Object, storageService.Object, photoAlbumsRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => service.UpdatePhotoAsync(photo.Id, visitor.Id, new EditPhotoViewModel()).Wait());

            Assert.Contains("Such photo does not exists", ex.Message);
        }

        [Fact]
        public void Update_ShouldThrowExceptionWhenPhotoDoesNotExist()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, false);

            var list = new List<Photo>();
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(list);
            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(new List<PhotoAlbum>());
            var storageService = PhotoStorageServiceMock.Get();

            var service = new PhotoService(photosRepo.Object, storageService.Object, photoAlbumsRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => service.UpdatePhotoAsync(photo.Id, user.Id, new EditPhotoViewModel()).Wait());

            Assert.Contains("Such photo does not exists", ex.Message);
        }

        [Fact]
        public void Delete_ShouldThrowExceptionWhenPhotoDoesNotExists()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, false);

            var list = new List<Photo>();
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(list);
            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(new List<PhotoAlbum>());
            var storageService = PhotoStorageServiceMock.Get();

            var service = new PhotoService(photosRepo.Object, storageService.Object, photoAlbumsRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => service.DeletePhotoAsync(photo.Id, user.Id).Wait());

            Assert.Contains("Such photo does not exists", ex.Message);
        }

        [Fact]
        public void Delete_ShouldThrowExceptionWhenUserIsNotOwner()
        {
            var user = UserCreator.Create("test");
            var visitor = UserCreator.Create("visitor");
            var photo = PhotoCreator.Create(user, false, false);

            var list = new List<Photo>();
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(list);
            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(new List<PhotoAlbum>());
            var storageService = PhotoStorageServiceMock.Get();

            var service = new PhotoService(photosRepo.Object, storageService.Object, photoAlbumsRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => service.DeletePhotoAsync(photo.Id, visitor.Id).Wait());

            Assert.Contains("Such photo does not exists", ex.Message);
        }

        [Fact]
        public void Delete_ShouldDeletePhotoSuccessfully()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, false);

            var list = new List<Photo>() { photo };
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(list);
            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(new List<PhotoAlbum>());
            var storageService = PhotoStorageServiceMock.Get();

            var service = new PhotoService(photosRepo.Object, storageService.Object, photoAlbumsRepo.Object);
            service.DeletePhotoAsync(photo.Id, user.Id).Wait();

            Assert.Empty(list);
        }

        [Fact]
        public void GetById_ShouldNotReturnPrivatePhotoWhenUserIsNotOwner()
        {
            // TODO: To be implemented when figure out how to fix automapper error.
        }

        [Fact]
        public void GetById_ShouldReturnPhotoCorrectly()
        {
            // TODO: To be implemented when figure out how to fix automapper error.
        }

        [Fact]
        public void GetAllByUserId_ShouldReturnOnlyPublicPhotosWhenUserIsNotOwner()
        {
            // TODO: To be implemented when figure out how to fix automapper error.
        }

        [Fact]
        public void GetAllByUserId_ShouldReturnAllPhotosWhenUserIsOwner()
        {
            // TODO: To be implemented when figure out how to fix automapper error.
        }

        [Fact]
        public void GetLatestPublic_ShouldReturnOnlyPublicPhotos()
        {
            // TODO: To be implemented when figure out how to fix automapper error.
        }
    }
}
