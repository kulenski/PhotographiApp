namespace PhotographiApp.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using PhotographiApp.Web.ViewModels.Photos;
    using PhotographiApp.Web.ViewModels.Topic;

    public class IndexViewModel
    {
        public ICollection<PhotoViewModel> LatestPhotos { get; set; }

        public ICollection<TopicViewModel> LatestTopics { get; set; }
    }
}
