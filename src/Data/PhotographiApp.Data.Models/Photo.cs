namespace PhotographiApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using PhotographiApp.Data.Common.Models;

    public class Photo : BaseDeletableModel<string>, IAuditInfo
    {
        public Photo()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Href { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsCommentAllowed { get; set; }

        public long GroupId { get; set; }

        public virtual Group Group { get; set; }

        public int LicenseId { get; set; }

        public virtual License License { get; set; }

        public int MetadataId { get; set; }

        public virtual PhotoMetadata Metadata { get; set; }

        public virtual ICollection<PhotoAlbum> Albums { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<PhotoFavorite> Favorites { get; set; }
    }
}
