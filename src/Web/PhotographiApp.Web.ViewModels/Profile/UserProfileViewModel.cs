namespace PhotographiApp.Web.ViewModels.Profile
{
    using System;
    using System.Collections.Generic;

    using PhotographiApp.Web.ViewModels.Albums;
    using PhotographiApp.Web.ViewModels.Photos;

    public class UserProfileViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<PhotoViewModel> Photos { get; set; }

        public ICollection<AlbumViewModel> Albums { get; set; }

        public ICollection<PhotoViewModel> FavoritePhotos { get; set; }
    }
}
