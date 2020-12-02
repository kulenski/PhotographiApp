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
            var viewModel = new UploadPhotoInputModel();
            var licenses = this.licenseService.GetAll();
            viewModel.Licenses = licenses;

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(UploadPhotoInputModel model)
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
                await this.photoService.CreatePhotoAsync(user.Id, null, $"{this.hostingEnvironment.WebRootPath}/images", model);
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

        [HttpGet]
        public IActionResult Show()
        {
            return null;
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Edit([FromBody]object model)
        {
            return null;
        }
    }
}
