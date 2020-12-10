namespace PhotographiApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using PhotographiApp.Data.Common.Repositories;
    using PhotographiApp.Data.Models;
    using PhotographiApp.Web.ViewModels.Albums;
    using Xunit;

    public class AlbumsServiceTests
    {
        [Fact]
        public void Create_ShouldCreateAlbumSuccessfully()
        {
            var list = new List<Album>();
            var photosRepo = new Mock<IRepository<PhotoAlbum>>();
            var albumsRepo = new Mock<IDeletableEntityRepository<Album>>();
            albumsRepo.Setup(x => x.AddAsync(It.IsAny<Album>())).Callback(
                (Album album) => list.Add(album));

            var services = new AlbumsService(albumsRepo.Object, photosRepo.Object);
            services.CreateAsync(new CreateAlbumInputModel() { Description = "TestDesc", IsPrivate = false, Name = "testName" }, "testUser").Wait();

            Assert.Single(list);
        }

        [Fact]
        public void Update_ShouldUpdateAlbumSucessfully()
        {
            var list = new List<Album>()
            {
                new Album()
                {
                    Id = "Id",
                    OwnerId = "Owner",
                    Description = "Test",
                    IsPrivate = true,
                },
            };

            var albumsRepo = new Mock<IDeletableEntityRepository<Album>>();
            var photosRepo = new Mock<IRepository<PhotoAlbum>>();
            albumsRepo.Setup(x => x.All()).Returns(list.AsQueryable());

            var services = new AlbumsService(albumsRepo.Object, photosRepo.Object);
            services.UpdateAsync(
                "Id",
                "Owner",
                new EditAlbumInputModel
                {
                    Id = "Id",
                    OwnerId = "Owner",
                    Description = "Updated",
                    IsPrivate = false,
                    Name = "Updated",
                }).Wait();

            var item = list.First();

            Assert.Equal("Updated", item.Description);
            Assert.Equal("Updated", item.Name);
        }

        [Fact]
        public void Update_ShouldNotUpdateAlbumSuccessfully()
        {
            var list = new List<Album>();
            var albumsRepo = new Mock<IDeletableEntityRepository<Album>>();
            var photosRepo = new Mock<IRepository<PhotoAlbum>>();
            albumsRepo.Setup(x => x.All()).Returns(list.AsQueryable());

            var services = new AlbumsService(albumsRepo.Object, photosRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => services.UpdateAsync("1", "2", new EditAlbumInputModel { Id = "1", OwnerId = "2", Description = "test", IsPrivate = false, Name = "test" }).Wait());

            Assert.Contains("Album does not exist!", ex.Message);
        }

        [Fact]
        public void Delete_ShoudNotDeleteWhenThereArePhotosAssociatedWithAlbum()
        {
        }

        [Fact]
        public void Delete_ShouldNotDeleteWhenAlbumIsDoesNotExists()
        {
        }

        [Fact]
        public void Delete_ShouldDeleteSuccessfully()
        {
        }

        [Fact]
        public void GetById_ShouldReturnCorrectData()
        {
        }

        [Fact]
        public void GetById_ShouldReturnNullDataWhenAlbumDoesNotExists()
        {
        }

        [Fact]
        public void GetAlbumPhotos_ShouldThrowErrorWhenAlbumDoesNotExists()
        {
        }

        [Fact]
        public void GetAlbumPhotos_ShouldReturnPublicPhotosWhenUserIsNotOwner()
        {
        }

        [Fact]
        public void GetAlbumPhotos_ShouldReturnAllPhotosWhenUserIsOwner()
        {
        }

        [Fact]
        public void GetUserAlbums_ShouldReturnAllAlbumsWhereUserIsOwner()
        {
        }

        [Fact]
        public void GetUserAlbums_ShouldReturnPublicAlbumsWhereUserIsNotOwner()
        {
        }
    }
}
