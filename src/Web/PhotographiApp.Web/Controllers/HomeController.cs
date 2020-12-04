namespace PhotographiApp.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using PhotographiApp.Services.Data;
    using PhotographiApp.Web.ViewModels;
    using PhotographiApp.Web.ViewModels.Photos;

    public class HomeController : BaseController
    {
        private readonly IPhotoService photoService;

        public HomeController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public IActionResult Index()
        {
            var latestPhotos = this.photoService.GetLatestPublic<PhotoViewModel>();
            return this.View(latestPhotos);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
