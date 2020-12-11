namespace PhotographiApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Tests.Mock;
    using PhotographiApp.Services.Data.Tests.Seed;
    using Xunit;

    public class FavoritesServiceTests
    {
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
            var favorites = new List<PhotoFavorite>() { new PhotoFavorite() { Photo = photo, User = visitorUser, PhotoId = photo.Id,  UserId = visitorUser.Id } };

            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo });
            var favoritesRepo = EfRepositoryMock.Get<PhotoFavorite>(favorites);

            var service = new FavoritesService(favoritesRepo.Object, photosRepo.Object);
            service.ToggleAsync(photo.Id, visitorUser.Id).Wait();

            Assert.Empty(favorites);
        }

        [Fact]
        public void GetUserFavoritePhotos_ShouldReturnFavoritePhotos()
        {
            // TODO: To be implemented when figure out how to fix automapper error.
        }

        [Fact]
        public void UserHasFavoritePhoto_ShouldReturnTrueWhenPhotoIsFavoriteToTheUser()
        {
            var user = UserCreator.Create("test");
            var visitorUser = UserCreator.Create("visitor");
            var photo = PhotoCreator.Create(user, false, false);
            var favorites = new List<PhotoFavorite>() { new PhotoFavorite() { Photo = photo, User = visitorUser, PhotoId = photo.Id, UserId = visitorUser.Id } };

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
            var favorites = new List<PhotoFavorite>() { new PhotoFavorite() { Photo = photo, User = visitorUser, PhotoId = photo.Id, UserId = visitorUser.Id } };

            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo });
            var favoritesRepo = EfRepositoryMock.Get<PhotoFavorite>(favorites);

            var service = new FavoritesService(favoritesRepo.Object, photosRepo.Object);
            var actual = service.UserHasFavoritePhoto(photo.Id, user.Id);

            Assert.False(actual);
        }
    }
}
