namespace PhotographiApp.Services.Data.Tests
{
    using Xunit;

    public class FavoritesServiceTests
    {
        [Fact]
        public void Toggle_ShouldThrowErrorWhenPhotoDoesNotExists()
        {
        }

        [Fact]
        public void Toggle_ShouldNotMakeFavoriteWhenUserOwnsThePhoto()
        {
        }

        [Fact]
        public void Toggle_ShouldMakeFavoriteWhenMappingDoesNotExist()
        {
        }

        [Fact]
        public void Toggle_ShouldRemoveFavoriteWhenMappingExists()
        {
        }

        [Fact]
        public void GetUserFavoritePhotos_ShouldReturnFavoritePhotos()
        {
        }

        [Fact]
        public void UserHasFavoritePhoto_ShouldReturnTrueWhenPhotoIsFavoriteToTheUser()
        {
        }

        [Fact]
        public void UserHasFavoritePhoto_ShouldFalseWhenPhotoIsNotFavoriteToTheUser()
        {
        }
    }
}
