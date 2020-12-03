namespace PhotographiApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using PhotographiApp.Data.Common.Models;
    using PhotographiApp.Data.Models.Application;

    public class Album : BaseDeletableModel<string>, IAuditInfo
    {
        public Album()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Photos = new HashSet<PhotoAlbum>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<PhotoAlbum> Photos { get; set; }
    }
}
