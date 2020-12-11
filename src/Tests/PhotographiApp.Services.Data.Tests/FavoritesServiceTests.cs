namespace PhotographiApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Tests.Mock;
    using PhotographiApp.Services.Data.Tests.Seed;
    using PhotographiApp.Services.Mapping;
    using PhotographiApp.Web.ViewModels;
    using PhotographiApp.Web.ViewModels.Photos;
    using Xunit;

    public class FavoritesServiceTests
    {
        public FavoritesServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public void Toggle_ShouldThrowErrorWhenPhotoDoesNotExists()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, false);
            var favorites = new List<PhotoFavorite>();

            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>());
            var favoritesRepo = EfRepositoryMock.Get<PhotoFavorite>(favorites);

            var service = new FavoritesService(favoritesRepo.Object, photosRepo.Object);
            Exception ex = Assert.Throws<AggregateException>(() => service.ToggleAsync(photo.Id, user.Id).Wait());

            Assert.Contains("Photo does not exist", ex.Message);
        }

        [Fact]
        public void Toggle_ShouldNotMakeFavoriteWhenUserOwnsThePhoto()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, false);
            var favorites = new List<PhotoFavorite>();

            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo });
            var favoritesRepo = EfRepositoryMock.Get<PhotoFavorite>(favorites);

            var service = new FavoritesService(favoritesRepo.Object, photosRepo.Object);
            service.ToggleAsync(photo.Id, user.Id).Wait();

            Assert.Empty(favorites);
        }

        [Fact]
        public void Toggle_ShouldMakeFavoriteWhenMappingDoesNotExist()
        {
            var user = UserCreator.Create("test");
            var visitorUser = UserCreator.Create("visitor");
            var photo = PhotoCreator.Create(user, false, false);
            var favorites = new List<PhotoFavorite>();

            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo });
            var favoritesRepo = EfRepositoryMock.Get<PhotoFavorite>(favorites);

            var service = new FavoritesService(favoritesRepo.Object, photosRepo.Object);
            service.ToggleAsync(photo.Id, visitorUser.Id).Wait();

            Assert.Single(favorites);
        }

        [Fact]
        public void Toggle_ShouldRemoveFavoriteWhenMappingExists()
        {
            var user = UserCreator.Create("test");
            var visitorUser = UserCreator.Create("visitor");
            var photo = PhotoCreator.Create(user, false, false);
            var favorites = new List<PhotoFavorite>() { PhotoFavoriteCreator.Create(photo, visitorUser) };

            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo });
            var favoritesRepo = EfRepositoryMock.Get<PhotoFavorite>(favorites);

            var service = new FavoritesService(favoritesRepo.Object, photosRepo.Object);
            service.ToggleAsync(photo.Id, visitorUser.Id).Wait();

            Assert.Empty(favorites);
        }

        [Fact]
        public void GetUserFavoritePhotos_ShouldReturnFavoritePhotos()
        {
            var user = UserCreator.Create("test");
            var visitor = UserCreator.Create("visitor");
            var photo1 = PhotoCreator.Create(user, false, false);
            var photo2 = PhotoCreator.Create(user, false, false);
            var photo3 = PhotoCreator.Create(visitor, false, false);

            var favorites = new List<PhotoFavorite>()
            {
                PhotoFavoriteCreator.Create(photo1, visitor),
                PhotoFavoriteCreator.Create(photo2, visitor),
                PhotoFavoriteCreator.Create(photo3, user),
            };

            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo1, photo2, photo3 });
            var favoritesRepo = EfRepositoryMock.Get<PhotoFavorite>(favorites);

            var service = new FavoritesService(favoritesRepo.Object, photosRepo.Object);
            var userFavorites = service.GetUserFavoritePhotos<PhotoViewModel>(user.Id);
            var visitorFavorites = service.GetUserFavoritePhotos<PhotoViewModel>(visitor.Id);

            Assert.Single(userFavorites);
            Assert.Equal(2, visitorFavorites.Count);
        }

        [Fact]
        public void UserHasFavoritePhoto_ShouldReturnTrueWhenPhotoIsFavoriteToTheUser()
        {
            var user = UserCreator.Create("test");
            var visitorUser = UserCreator.Create("visitor");
            var photo = PhotoCreator.Create(user, false, false);
            var favorites = new List<PhotoFavorite>() { PhotoFavoriteCreator.Create(photo, visitorUser) };

            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo });
            var favoritesRepo = EfRepositoryMock.Get<PhotoFavorite>(favorites);

            var service = new FavoritesService(favoritesRepo.Object, photosRepo.Object);
            var actual = service.UserHasFavoritePhoto(photo.Id, visitorUser.Id);

            Assert.True(actual);
        }

        [Fact]
        public void UserHasFavoritePhoto_ShouldFalseWhenPhotoIsNotFavoriteToTheUser()
        {
            var user = UserCreator.Create("test");
            var visitorUser = UserCreator.Create("visitor");
            var photo = PhotoCreator.Create(user, false, false);
            var favorites = new List<PhotoFavorite>() { PhotoFavoriteCreator.Create(photo, visitorUser) };

            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo });
            var favoritesRepo = EfRepositoryMock.Get<PhotoFavorite>(favorites);

            var service = new FavoritesService(favoritesRepo.Object, photosRepo.Object);
            var actual = service.UserHasFavoritePhoto(photo.Id, user.Id);

            Assert.False(actual);
        }
    }
}
