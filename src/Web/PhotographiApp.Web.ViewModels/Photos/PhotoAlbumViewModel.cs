namespace PhotographiApp.Web.ViewModels.Photos
{
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Mapping;

    public class PhotoAlbumViewModel : IMapFrom<PhotoAlbum>
    {
        public string AlbumId { get; set; }

        public string AlbumName { get; set; }

        public string AlbumDescription { get; set; }
    }
}
