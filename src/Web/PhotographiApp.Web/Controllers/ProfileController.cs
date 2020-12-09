namespace PhotographiApp.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PhotographiApp.Data.Models.Application;
    using PhotographiApp.Services.Data.Interfaces;
    using PhotographiApp.Web.ViewModels.Albums;
    using PhotographiApp.Web.ViewModels.Photos;
    using PhotographiApp.Web.ViewModels.Profile;

    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IAlbumsService albumsService;
        private readonly IPhotoService photoService;
        private readonly IFavoritesService favoritesService;
        private readonly UserManager<User> userManager;

        public ProfileController(
            IAlbumsService albumsService,
            IPhotoService photoService,
            IFavoritesService favoritesService,
            UserManager<User> userManager)
        {
            this.albumsService = albumsService;
            this.photoService = photoService;
            this.favoritesService = favoritesService;
            this.userManager = userManager;
        }

        public IActionResult Show(string id)
        {
            var viewModel = new UserProfileViewModel();
            var user = this.userManager.Users.Where(x => x.Id == id).FirstOrDefault();
            var currentUser = this.userManager.GetUserId(this.User);
            if (user == null)
            {
                this.ViewData["Error"] = "User does not exist!";
                return this.View("ValidationError");
            }

            var photos = this.photoService.GetAllByUserId<PhotoViewModel>(user.Id, currentUser);
            var albums = this.albumsService.GetUserAlbums<AlbumViewModel>(user.Id, currentUser);
            var favoritePhotos = this.favoritesService.GetUserFavoritePhotos<PhotoViewModel>(user.Id);

            viewModel.Albums = albums;
            viewModel.Photos = photos;
            viewModel.FavoritePhotos = favoritePhotos;
            viewModel.UserName = user.UserName;
            viewModel.CreatedOn = user.CreatedOn;
            viewModel.Id = user.Id;

            return this.View(viewModel);
        }
    }
}
