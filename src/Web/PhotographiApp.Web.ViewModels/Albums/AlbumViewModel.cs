namespace PhotographiApp.Web.ViewModels.Albums
{
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Mapping;

    public class AlbumViewModel : IMapFrom<Album>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPrivate { get; set; }
    }
}
