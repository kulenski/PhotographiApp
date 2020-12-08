namespace PhotographiApp.Web.ViewModels.PhotoAlbum
{
    using System.Collections.Generic;

    using PhotographiApp.Web.ViewModels.Albums;

    public class AddRemoveAlbumViewModel
    {
        public string AlbumId { get; set; }

        public string PhotoId { get; set; }

        public string PhotoTitle { get; set; }

        public IEnumerable<AlbumViewModel> Albums { get; set; }
    }
}
