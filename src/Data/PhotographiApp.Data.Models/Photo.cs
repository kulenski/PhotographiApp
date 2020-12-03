namespace PhotographiApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using PhotographiApp.Data.Common.Models;
    using PhotographiApp.Data.Models.Application;

    public class Photo : BaseDeletableModel<string>, IAuditInfo
    {
        public Photo()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Albums = new HashSet<PhotoAlbum>();
            this.Comments = new HashSet<Comment>();
            this.Favorites = new HashSet<PhotoFavorite>();
            this.Tags = new HashSet<PhotoTag>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Href { get; set; }

        public string ThumbnailHref { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsCommentAllowed { get; set; }

        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public string LicenseId { get; set; }

        public virtual License License { get; set; }

        public string Camera { get; set; }

        public string Lens { get; set; }

        public string ExposureTime { get; set; }

        public string Aperture { get; set; }

        public string Iso { get; set; }

        public string Flash { get; set; }

        public DateTime? DateTaken { get; set; }

        public virtual ICollection<PhotoAlbum> Albums { get; set; }

        public virtual ICollection<PhotoTag> Tags { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<PhotoFavorite> Favorites { get; set; }
    }
}
