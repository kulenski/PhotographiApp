namespace PhotographiApp.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PhotographiApp.Services.Data.Interfaces;
    using PhotographiApp.Web.ViewModels;
    using PhotographiApp.Web.ViewModels.Home;
    using PhotographiApp.Web.ViewModels.Photos;
    using PhotographiApp.Web.ViewModels.Topic;

    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IPhotoService photoService;
        private readonly ITopicService topicService;

        public HomeController(
            IPhotoService photoService,
            ITopicService topicService)
        {
            this.photoService = photoService;
            this.topicService = topicService;
        }

        public IActionResult Index()
        {
            var latestPhotos = this.photoService.GetLatestPublic<PhotoViewModel>();
            var latestTopics = this.topicService.GetLatest<TopicViewModel>();
            var viewModel = new IndexViewModel()
            {
                LatestPhotos = latestPhotos,
                LatestTopics = latestTopics,
            };

            return this.View(viewModel);
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
