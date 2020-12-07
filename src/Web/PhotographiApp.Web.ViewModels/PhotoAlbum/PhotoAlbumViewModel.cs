namespace PhotographiApp.Web.ViewModels.PhotoAlbum
{
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Mapping;

    public class PhotoAlbumViewModel : IMapFrom<PhotoAlbum>
    {
        public string AlbumId { get; set; }

        public string AlbumName { get; set; }

        public string AlbumDescription { get; set; }

        public string PhotoId { get; set; }

        public string PhotoTitle { get; set; }

        public string PhotoDescription { get; set; }

        public string PhotoHref { get; set; }

        public string PhotoThumbnailHref { get; set; }
    }
}
