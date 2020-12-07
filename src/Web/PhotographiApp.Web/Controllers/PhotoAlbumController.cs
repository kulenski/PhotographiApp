namespace PhotographiApp.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PhotographiApp.Data.Models.Application;
    using PhotographiApp.Services.Data;
    using PhotographiApp.Web.ViewModels.Albums;
    using PhotographiApp.Web.ViewModels.PhotoAlbum;
    using PhotographiApp.Web.ViewModels.Photos;

    [Authorize]
    public class PhotoAlbumController : BaseController
    {
        private readonly IPhotoService photoService;
        private readonly IPhotoAlbumService photoAlbumService;
        private readonly UserManager<User> userManager;

        public PhotoAlbumController(
            IPhotoService photoService,
            IPhotoAlbumService photoAlbumService,
            UserManager<User> userManager)
        {
            this.photoService = photoService;
            this.photoAlbumService = photoAlbumService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Add(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var photo = this.photoService.GetById<PhotoViewModel>(id, userId);
            var albums = this.photoAlbumService.GetAllUnusedAlbums<AlbumViewModel>(userId, userId);

            if (photo == null)
            {
                this.ViewData["Error"] = "Photo not found!";
                return this.View("Error");
            }

            var viewModel = new AddRemoveAlbumViewModel()
            {
                Albums = albums,
                PhotoId = photo.Id,
                PhotoTitle = photo.Title,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRemoveAlbumInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            try
            {
                await this.photoAlbumService.AddPhotoAsync(model.AlbumId, model.PhotoId, userId);
            }
            catch (Exception ex)
            {
                this.ViewData["Error"] = ex.Message;
                return this.View("Error");
            }

            return this.RedirectToAction("Show", "Photo", new { Id = model.PhotoId });
        }

        [HttpGet]
        public IActionResult Remove(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var photo = this.photoService.GetById<PhotoViewModel>(id, userId);
            var albums = this.photoAlbumService.GetAllUsedAlbums<AlbumViewModel>(id, userId);

            if (photo == null)
            {
                this.ViewData["Error"] = "Photo not found!";
                return this.View("Error");
            }

            var viewModel = new AddRemoveAlbumViewModel()
            {
                Albums = albums,
                PhotoId = photo.Id,
                PhotoTitle = photo.Title,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(AddRemoveAlbumInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);
            try
            {
                await this.photoAlbumService.RemovePhotoAsync(model.AlbumId, model.PhotoId, userId);
            }
            catch (Exception ex)
            {
                this.ViewData["Error"] = ex.Message;
                return this.View("Error");
            }

            return this.RedirectToAction("Show", "Photo", model.PhotoId);
        }
    }
}
