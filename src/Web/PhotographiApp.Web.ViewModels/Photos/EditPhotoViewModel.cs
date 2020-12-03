namespace PhotographiApp.Web.ViewModels.Photos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Mapping;

    public class EditPhotoViewModel : IMapFrom<Photo>
    {
        [Required]
        [MinLength(4)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string LicenseId { get; set; }

        [Required]
        public bool IsPrivate { get; set; }

        [Required]
        public bool IsCommentAllowed { get; set; }

        public ICollection<KeyValuePair<string, string>> Licenses { get; set; }
    }
}
