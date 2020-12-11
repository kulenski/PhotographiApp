namespace PhotographiApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Tests.Mock;
    using PhotographiApp.Services.Data.Tests.Seed;
    using Xunit;

    public class PhotoAlbumServiceTests
    {
        [Fact]
        public void AddPhoto_ShouldThrowErrorWhenPhotoDoesNotExist()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, false);
            var album = AlbumCreator.Create(false, user);
            var list = new List<PhotoAlbum>();

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(list);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(new List<Album>() { album });
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>());

            var service = new PhotoAlbumService(photosRepo.Object, albumsRepo.Object, photoAlbumsRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => service.AddPhotoAsync(album.Id, photo.Id, user.Id).Wait());

            Assert.Contains("Photo does not exist", ex.Message);
        }

        [Fact]
        public void AddPhoto_ShouldThrowErrorWhenAlbumDoesNotExist()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, false);
            var album = AlbumCreator.Create(false, user);
            var list = new List<PhotoAlbum>();

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(list);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(new List<Album>());
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo });

            var service = new PhotoAlbumService(photosRepo.Object, albumsRepo.Object, photoAlbumsRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => service.AddPhotoAsync(album.Id, photo.Id, user.Id).Wait());

            Assert.Contains("Album does not exist", ex.Message);
        }

        [Fact]
        public void AddPhoto_ShouldThrowErrorWhenPhotoAlreadyInTheAlbum()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, false);
            var album = AlbumCreator.Create(false, user);
            var mapping = PhotoAlbumCreator.Create(photo, album);
            var list = new List<PhotoAlbum>() { mapping };

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(list);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(new List<Album>() { album });
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo });

            var service = new PhotoAlbumService(photosRepo.Object, albumsRepo.Object, photoAlbumsRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => service.AddPhotoAsync(album.Id, photo.Id, user.Id).Wait());

            Assert.Contains("Photo is already added to the album", ex.Message);
        }

        [Fact]
        public void AddPhoto_ShouldAddSuccessfully()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, false);
            var album = AlbumCreator.Create(false, user);
            var list = new List<PhotoAlbum>();

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(list);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(new List<Album>() { album });
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo });

            var service = new PhotoAlbumService(photosRepo.Object, albumsRepo.Object, photoAlbumsRepo.Object);
            service.AddPhotoAsync(album.Id, photo.Id, user.Id).Wait();

            Assert.Single(list);
        }

        [Fact]
        public void RemovePhoto_ShouldThrowErrorWhenPhotoDoesNotExist()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, false);
            var album = AlbumCreator.Create(false, user);
            var list = new List<PhotoAlbum>();

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(list);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(new List<Album>() { album });
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>());

            var service = new PhotoAlbumService(photosRepo.Object, albumsRepo.Object, photoAlbumsRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => service.RemovePhotoAsync(album.Id, photo.Id, user.Id).Wait());

            Assert.Contains("Photo does not exist", ex.Message);
        }

        [Fact]
        public void RemovePhoto_ShouldThrowErrorWhenAlbumDoesNotExist()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, false);
            var album = AlbumCreator.Create(false, user);
            var list = new List<PhotoAlbum>();

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(list);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(new List<Album>());
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo });

            var service = new PhotoAlbumService(photosRepo.Object, albumsRepo.Object, photoAlbumsRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => service.RemovePhotoAsync(album.Id, photo.Id, user.Id).Wait());

            Assert.Contains("Album does not exist", ex.Message);
        }

        [Fact]
        public void RemovePhoto_ShouldThrowErrorWhenMappingDoesNotExist()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, false);
            var album = AlbumCreator.Create(false, user);
            var list = new List<PhotoAlbum>();

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(list);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(new List<Album>() { album });
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo });

            var service = new PhotoAlbumService(photosRepo.Object, albumsRepo.Object, photoAlbumsRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => service.RemovePhotoAsync(album.Id, photo.Id, user.Id).Wait());

            Assert.Contains("Such mapping between photo and album does not exist", ex.Message);
        }

        [Fact]
        public void RemovPhoto_ShouldRemoveSuccessfully()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, false);
            var album = AlbumCreator.Create(false, user);
            var mapping = PhotoAlbumCreator.Create(photo, album);
            var list = new List<PhotoAlbum>() { mapping };

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(list);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(new List<Album>() { album });
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo });

            var service = new PhotoAlbumService(photosRepo.Object, albumsRepo.Object, photoAlbumsRepo.Object);
            service.RemovePhotoAsync(album.Id, photo.Id, user.Id).Wait();

            Assert.Empty(list);
        }

        [Fact]
        public void GetAllUnusedAlbums_ShouldReturnCorrectNumber()
        {
            // TODO: To be implemented when figure out how to fix automapper error.
        }

        [Fact]
        public void GetAllUsedAlbums_ShouldReturnCorrectNumber()
        {
            // TODO: To be implemented when figure out how to fix automapper error.
        }

        [Fact]
        public void GetAllByPhotoId_ShouldReturnCorrectNumber()
        {
            // TODO: To be implemented when figure out how to fix automapper error.
        }
    }
}
