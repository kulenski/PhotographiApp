namespace PhotographiApp.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PhotographiApp.Data.Models.Application;
    using PhotographiApp.Services.Data;
    using PhotographiApp.Web.ViewModels.Albums;
    using PhotographiApp.Web.ViewModels.Photos;
    using PhotographiApp.Web.ViewModels.Profile;

    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IAlbumsService albumsService;
        private readonly IPhotoService photoService;
        private readonly UserManager<User> userManager;

        public ProfileController(
            IAlbumsService albumsService,
            IPhotoService photoService,
            UserManager<User> userManager)
        {
            this.albumsService = albumsService;
            this.photoService = photoService;
            this.userManager = userManager;
        }

        public IActionResult Show(string id)
        {
            var viewModel = new UserProfileViewModel();
            var user = this.userManager.Users.Where(x => x.Id == id).FirstOrDefault();
            if (user == null)
            {
                return this.View("NotFound");
            }

            var photos = this.photoService.GetAllByUserId<PhotoViewModel>(user.Id);
            var albums = this.albumsService.GetUserAlbums<AlbumViewModel>(user.Id);

            viewModel.Albums = albums;
            viewModel.Photos = photos;
            viewModel.UserName = user.UserName;
            viewModel.CreatedOn = user.CreatedOn;

            return this.View(viewModel);
        }
    }
}
