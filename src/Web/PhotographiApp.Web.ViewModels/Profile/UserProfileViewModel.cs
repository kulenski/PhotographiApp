namespace PhotographiApp.Web.ViewModels.Profile
{
    using System;
    using System.Collections.Generic;

    using PhotographiApp.Web.ViewModels.Albums;
    using PhotographiApp.Web.ViewModels.Photos;
    using PhotographiApp.Web.ViewModels.Topic;

    public class UserProfileViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<PhotoViewModel> Photos { get; set; }

        public ICollection<AlbumViewModel> Albums { get; set; }

        public ICollection<PhotoViewModel> FavoritePhotos { get; set; }

        public ICollection<TopicViewModel> Topics { get; set; }
    }
}
