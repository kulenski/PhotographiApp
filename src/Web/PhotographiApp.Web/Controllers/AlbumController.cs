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

    [Authorize]
    public class AlbumController : BaseController
    {
        private readonly IAlbumsService albumsService;
        private readonly UserManager<User> userManager;

        public AlbumController(IAlbumsService albumsService, UserManager<User> userManager)
        {
            this.albumsService = albumsService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateAlbumInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAlbumInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.albumsService.CreateAsync(model, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            return this.View("CreateSuccessful");
        }

        public IActionResult Show(string id)
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            string userId = null;

            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                userId = user.Id;
            }

            var album = this.albumsService.GetById<EditAlbumInputModel>(id, userId);
            if (album == null)
            {
                this.ViewData["Error"] = "Album not found!";
                return this.View("AlbumLoadingError");
            }

            return this.View(album);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditAlbumInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                var user = await this.userManager.GetUserAsync(this.User);
                await this.albumsService.UpdateAsync(id, user.Id, model);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            return this.RedirectToAction("Show", "Album", new { Id = id });
        }

        public async Task<IActionResult> Delete(string id)
        {
            string userId = null;

            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                userId = user.Id;
            }

            try
            {
                await this.albumsService.DeleteAsync(id, userId);
                return this.RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                this.ViewData["Error"] = ex.Message;
                return this.View("AlbumLoadingError");
            }
        }
    }
}
