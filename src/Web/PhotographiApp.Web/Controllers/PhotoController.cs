namespace PhotographiApp.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PhotographiApp.Data.Models.Application;
    using PhotographiApp.Services.Data;
    using PhotographiApp.Web.ViewModels.Photos;

    [Authorize]
    public class PhotoController : BaseController
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly UserManager<User> userManager;
        private readonly ILicenseService licenseService;
        private readonly IPhotoService photoService;

        public PhotoController(
            IWebHostEnvironment hostingEnvironment,
            UserManager<User> userManager,
            ILicenseService licenseService,
            IPhotoService photoService)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            this.licenseService = licenseService;
            this.photoService = photoService;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            var viewModel = new CreatePhotoInputModel();
            var licenses = this.licenseService.GetAll();
            viewModel.Licenses = licenses;

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(CreatePhotoInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var licenses = this.licenseService.GetAll();
                model.Licenses = licenses;
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.photoService.CreatePhotoAsync(user.Id, $"{this.hostingEnvironment.WebRootPath}/images", model);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                var licenses = this.licenseService.GetAll();
                model.Licenses = licenses;
                return this.View(model);
            }

            return this.Redirect("/Photo/UploadSuccessful");
        }

        public IActionResult UploadSuccessful()
        {
            return this.View();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Show(string id)
        {
            string userId = null;

            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                userId = user.Id;
            }

            var model = this.photoService.GetById<PhotoViewModel>(id, userId);
            if (model == null)
            {
                this.ViewData["Error"] = "Photo not found!";
                return this.View("PhotoLoadingError");
            }

            if (model.OwnerId == userId)
            {
                model.IsOwnerByCurrentUser = true;
            }
            else
            {
                model.IsOwnerByCurrentUser = false;
            }

            return this.View(model);
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

            var model = this.photoService.GetById<EditPhotoViewModel>(id, userId);
            if (model == null)
            {
                this.ViewData["Error"] = "Photo not found!";
                return this.View("PhotoLoadingError");
            }

            var licenses = this.licenseService.GetAll();
            model.Licenses = licenses;

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditPhotoViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var licenses = this.licenseService.GetAll();
                model.Licenses = licenses;

                return this.View(model);
            }

            try
            {
                var user = await this.userManager.GetUserAsync(this.User);
                await this.photoService.UpdatePhotoAsync(id, user.Id, model);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                var licenses = this.licenseService.GetAll();
                model.Licenses = licenses;
                return this.View(model);
            }

            return this.RedirectToAction("Show", "Photo", new { Id = id });
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
                await this.photoService.DeletePhotoAsync(id, userId);
                return this.RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                this.ViewData["Error"] = ex.Message;
                return this.View("PhotoLoadingError");
            }
        }
    }
}
