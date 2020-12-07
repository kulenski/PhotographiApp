namespace PhotographiApp.Web.ViewModels.Albums
{
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Mapping;

    public class EditAlbumInputModel : IMapFrom<Album>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        public string OwnerId { get; set; }
    }
}
