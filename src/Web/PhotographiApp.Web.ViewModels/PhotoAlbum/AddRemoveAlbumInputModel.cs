namespace PhotographiApp.Web.ViewModels.PhotoAlbum
{
    using System.ComponentModel.DataAnnotations;

    public class AddRemoveAlbumInputModel
    {
        [Required]
        public string AlbumId { get; set; }

        [Required]
        public string PhotoId { get; set; }
    }
}
