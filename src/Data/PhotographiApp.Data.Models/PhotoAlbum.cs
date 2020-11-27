namespace PhotographiApp.Data.Models
{
    using PhotographiApp.Data.Common.Models;

    public class PhotoAlbum : BaseModel<int>, IAuditInfo
    {
        public string PhotoId { get; set; }

        public virtual Photo Photo { get; set; }

        public string AlbumId { get; set; }

        public virtual Album Album { get; set; }
    }
}
