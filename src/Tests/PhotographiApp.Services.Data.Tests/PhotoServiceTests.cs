namespace PhotographiApp.Services.Data.Tests
{
    using Xunit;

    public class PhotoServiceTests
    {
        [Fact]
        public void Create_ShouldNotCreateWhenExtensionIsNotCorrect()
        {
        }

        [Fact]
        public void Create_ShouldCreateSuccessfully()
        {
        }

        [Fact]
        public void Update_ShouldNotUpdateWhenUserIsNotOwner()
        {
        }

        [Fact]
        public void Update_ShouldNotUpdateWhenPhotoDoesNotExist()
        {
        }

        [Fact]
        public void Update_ShouldUpdateSuccessfully()
        {
        }

        [Fact]
        public void Delete_ShouldNotDeleteWhenPhotoDoesNotExists()
        {
        }

        [Fact]
        public void Delete_ShouldDeletePhotoAndPhotoFavoritesSuccessfully()
        {
        }

        [Fact]
        public void GetById_ShouldNotReturnPrivatePhotoWhenUserIsNotOwner()
        {
        }

        [Fact]
        public void GetById_ShouldReturnPhotoCorrectly()
        {
        }

        [Fact]
        public void GetAllByUserId_ShouldReturnOnlyPublicPhotosWhenUserIsNotOwner()
        {
        }

        [Fact]
        public void GetAllByUserId_ShouldReturnAllPhotosWhenUserIsOwner()
        {
        }

        [Fact]
        public void GetLatestPublic_ShouldReturnOnlyPublicPhotos()
        {
        }
    }
}
