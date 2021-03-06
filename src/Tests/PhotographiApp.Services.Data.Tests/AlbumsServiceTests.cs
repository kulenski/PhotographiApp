﻿namespace PhotographiApp.Services.Data.Tests
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
    using PhotographiApp.Web.ViewModels.Albums;
    using PhotographiApp.Web.ViewModels.PhotoAlbum;
    using Xunit;

    public class AlbumsServiceTests
    {
        public AlbumsServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public void Create_ShouldCreateAlbumSuccessfully()
        {
            var list = new List<Album>();
            var model = new CreateAlbumInputModel() { Description = "TestDesc", IsPrivate = false, Name = "testName" };
            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(new List<PhotoAlbum>());
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(list);

            var services = new AlbumsService(albumsRepo.Object, photoAlbumsRepo.Object);
            services.CreateAsync(model, "testUser").Wait();

            Assert.Single(list);
        }

        [Fact]
        public void Update_ShouldUpdateAlbumSucessfully()
        {
            var user = UserCreator.Create("test");
            var album = AlbumCreator.Create(false, user);
            var list = new List<Album>()
            {
                album,
            };

            var editAlbumModel = new EditAlbumInputModel
            {
                Id = album.Id,
                OwnerId = user.Id,
                Description = "Updated",
                IsPrivate = false,
                Name = "Updated",
            };

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(new List<PhotoAlbum>());
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(list);

            var services = new AlbumsService(albumsRepo.Object, photoAlbumsRepo.Object);
            services.UpdateAsync(
               album.Id,
               user.Id,
               editAlbumModel).Wait();

            var item = list.First();

            Assert.Equal(editAlbumModel.Description, item.Description);
            Assert.Equal(editAlbumModel.Name, item.Name);
        }

        [Fact]
        public void Update_ShouldNotUpdateAlbumSuccessfully()
        {
            var list = new List<Album>();
            var model = new EditAlbumInputModel { Id = "1", OwnerId = "2", Description = "test", IsPrivate = false, Name = "test" };
            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(new List<PhotoAlbum>());
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(list);

            var services = new AlbumsService(albumsRepo.Object, photoAlbumsRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => services.UpdateAsync("1", "2", model).Wait());

            Assert.Contains("Album does not exist!", ex.Message);
        }

        [Fact]
        public void Delete_ShoudNotDeleteWhenThereArePhotosAssociatedWithAlbum()
        {
            var user = UserCreator.Create("test");
            var album = AlbumCreator.Create(false, user);
            var photo = PhotoCreator.Create(user, false, false);
            var photoAlbum = PhotoAlbumCreator.Create(photo, album);

            var listAlbums = new List<Album>() { album };
            var listPhotoAlbums = new List<PhotoAlbum>() { photoAlbum };

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(listPhotoAlbums);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(listAlbums);

            var services = new AlbumsService(albumsRepo.Object, photoAlbumsRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => services.DeleteAsync(album.Id, user.Id).Wait());

            Assert.Contains("Album contains photos!", ex.Message);
        }

        [Fact]
        public void Delete_ShouldNotDeleteWhenAlbumIsDoesNotExists()
        {
            var user = UserCreator.Create("test");
            var album = AlbumCreator.Create(false, user);
            var photo = PhotoCreator.Create(user, false, false);
            var photoAlbum = PhotoAlbumCreator.Create(photo, album);

            var listAlbums = new List<Album>();
            var listPhotoAlbums = new List<PhotoAlbum>();

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(listPhotoAlbums);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(listAlbums);

            var service = new AlbumsService(albumsRepo.Object, photoAlbumsRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => service.DeleteAsync(album.Id, user.Id).Wait());

            Assert.Contains("Album does not exist!", ex.Message);
        }

        [Fact]
        public void Delete_ShouldDeleteSuccessfully()
        {
            var user = UserCreator.Create("test");
            var album = AlbumCreator.Create(false, user);

            var listAlbums = new List<Album>() { album };
            var listPhotoAlbums = new List<PhotoAlbum>();

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(listPhotoAlbums);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(listAlbums);

            var service = new AlbumsService(albumsRepo.Object, photoAlbumsRepo.Object);
            service.DeleteAsync(album.Id, user.Id).Wait();

            Assert.Empty(listAlbums);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectData()
        {
            var user = UserCreator.Create("test");
            var album = AlbumCreator.Create(false, user);

            var listAlbums = new List<Album>() { album };
            var listPhotoAlbums = new List<PhotoAlbum>();

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(listPhotoAlbums);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(listAlbums);

            var service = new AlbumsService(albumsRepo.Object, photoAlbumsRepo.Object);
            var returnedAlbum = service.GetById<AlbumViewModel>(album.Id, user.Id);

            Assert.Equal(album.Id, returnedAlbum.Id);
            Assert.Equal(album.Name, returnedAlbum.Name);
            Assert.Equal(album.Description, returnedAlbum.Description);
        }

        [Fact]
        public void GetById_ShouldReturnNullDataWhenAlbumDoesNotExists()
        {
            var user = UserCreator.Create("test");
            var album = AlbumCreator.Create(false, user);

            var listAlbums = new List<Album>();
            var listPhotoAlbums = new List<PhotoAlbum>();

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(listPhotoAlbums);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(listAlbums);

            var service = new AlbumsService(albumsRepo.Object, photoAlbumsRepo.Object);
            var returnedAlbum = service.GetById<AlbumViewModel>(album.Id, user.Id);

            Assert.Null(returnedAlbum);
        }

        [Fact]
        public void GetAlbumPhotos_ShouldThrowErrorWhenAlbumDoesNotExists()
        {
            var user = UserCreator.Create("test");
            var album = AlbumCreator.Create(false, user);

            var listAlbums = new List<Album>();
            var listPhotoAlbums = new List<PhotoAlbum>();

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(listPhotoAlbums);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(listAlbums);

            var service = new AlbumsService(albumsRepo.Object, photoAlbumsRepo.Object);

            Exception ex = Assert.Throws<Exception>(() => service.GetAlbumPhotos<PhotoAlbumViewModel>(album.Id, user.Id));

            Assert.Contains("Album does not exist!", ex.Message);
        }

        [Fact]
        public void GetAlbumPhotos_ShouldReturnPublicPhotosWhenUserIsNotOwner()
        {
            var user = UserCreator.Create("test");
            var visitor = UserCreator.Create("visitor");
            var publicPhoto = PhotoCreator.Create(user, false, false);
            var privatePhoto = PhotoCreator.Create(user, true, false);
            var album = AlbumCreator.Create(false, user);

            var listAlbums = new List<Album>() { album };
            var listPhotoAlbums = new List<PhotoAlbum>()
            {
                PhotoAlbumCreator.Create(publicPhoto, album),
                PhotoAlbumCreator.Create(privatePhoto, album),
            };

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(listPhotoAlbums);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(listAlbums);

            var service = new AlbumsService(albumsRepo.Object, photoAlbumsRepo.Object);
            var photos = service.GetAlbumPhotos<PhotoAlbumViewModel>(album.Id, visitor.Id);

            Assert.Single(photos);
        }

        [Fact]
        public void GetAlbumPhotos_ShouldReturnAllPhotosWhenUserIsOwner()
        {
            var user = UserCreator.Create("test");
            var publicPhoto = PhotoCreator.Create(user, false, false);
            var privatePhoto = PhotoCreator.Create(user, true, false);
            var album = AlbumCreator.Create(false, user);

            var listAlbums = new List<Album>() { album };
            var listPhotoAlbums = new List<PhotoAlbum>()
            {
                PhotoAlbumCreator.Create(publicPhoto, album),
                PhotoAlbumCreator.Create(privatePhoto, album),
            };

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(listPhotoAlbums);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(listAlbums);

            var service = new AlbumsService(albumsRepo.Object, photoAlbumsRepo.Object);
            var photos = service.GetAlbumPhotos<PhotoAlbumViewModel>(album.Id, user.Id);

            Assert.Equal(2, photos.Count);
        }

        [Fact]
        public void GetUserAlbums_ShouldReturnAllAlbumsWhereUserIsOwner()
        {
            var user = UserCreator.Create("test");
            var privateAlbum = AlbumCreator.Create(true, user);
            var publicAlbum = AlbumCreator.Create(false, user);

            var listAlbums = new List<Album>() { privateAlbum, publicAlbum };
            var listPhotoAlbums = new List<PhotoAlbum>();

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(listPhotoAlbums);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(listAlbums);

            var service = new AlbumsService(albumsRepo.Object, photoAlbumsRepo.Object);
            var albums = service.GetUserAlbums<AlbumViewModel>(user.Id, user.Id);

            Assert.Equal(2, albums.Count);
        }

        [Fact]
        public void GetUserAlbums_ShouldReturnPublicAlbumsWhereUserIsNotOwner()
        {
            var user = UserCreator.Create("test");
            var visitor = UserCreator.Create("visitor");
            var privateAlbum = AlbumCreator.Create(true, user);
            var publicAlbum = AlbumCreator.Create(false, user);

            var listAlbums = new List<Album>() { privateAlbum, publicAlbum };
            var listPhotoAlbums = new List<PhotoAlbum>();

            var photoAlbumsRepo = EfRepositoryMock.Get<PhotoAlbum>(listPhotoAlbums);
            var albumsRepo = DeletableEntityRepositoryMock.Get<Album>(listAlbums);

            var service = new AlbumsService(albumsRepo.Object, photoAlbumsRepo.Object);
            var albums = service.GetUserAlbums<AlbumViewModel>(user.Id, visitor.Id);

            Assert.Single(albums);
        }
    }
}
