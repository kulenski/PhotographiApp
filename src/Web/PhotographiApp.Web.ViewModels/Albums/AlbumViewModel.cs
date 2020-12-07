namespace PhotographiApp.Web.ViewModels.Albums
{
    using System.Collections.Generic;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Mapping;
    using PhotographiApp.Web.ViewModels.PhotoAlbum;

    public class AlbumViewModel : IMapFrom<Album>
    {
        public AlbumViewModel()
        {
            this.IsOwnerByCurrentUser = false;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        public string OwnerId { get; set; }

        public bool IsOwnerByCurrentUser { get; set; }

        public ICollection<PhotoAlbumViewModel> Photos { get; set; }
    }
}
